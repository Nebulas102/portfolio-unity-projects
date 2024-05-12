using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBehaviour : MonoBehaviour
{
    private PlayerManager playerManager;

    private GameObject patroller;
    private PatrollerController patrollerController;
    private CreateSensor createSensor;
    
    private bool playerDetected = false;

    private void Start()
    {   
        playerManager = PlayerManager.instance;
        patroller = transform.root.gameObject;
        patrollerController = patroller.GetComponent<PatrollerController>();
        createSensor = patroller.GetComponent<CreateSensor>();

        gameObject.layer = patrollerController.ignoreLayer;

        transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    private void FixedUpdate()
    {
        bool isPlayerInFOV = IsPlayerInFOV();
        bool hasLOS = HasLineOfSight();

        if (isPlayerInFOV && hasLOS && playerDetected)
        {
            playerManager.GameOver();
        }
        else if (hasLOS || isPlayerInFOV)
        {
            playerDetected = true;

            if (hasLOS)
            {
                playerManager.playerInLOS();
            }
            else if (isPlayerInFOV)
            {
                playerManager.playerInFOV();
            }
        }
        else if (!hasLOS && !isPlayerInFOV && playerDetected)
        {
            playerDetected = false;
            playerManager.playerSafe();
        }   
    }

    public bool IsPlayerInFOV()
    {
        Vector3 directionToPlayer = playerManager.player.transform.position - transform.position;
        float signedAngleToPlayer = Vector3.SignedAngle(transform.forward, directionToPlayer, transform.up);
        float distanceToPlayer = Vector3.Distance(transform.position, playerManager.player.transform.position);

        if (Mathf.Abs(signedAngleToPlayer) <= createSensor.angle &&  distanceToPlayer <= createSensor.distance)
        {
            return true;
        }

        return false;
    }

    public bool HasLineOfSight()
    {
        Vector3 directionToPlayer = playerManager.player.transform.position - transform.position;
        float signedAngleToPlayer = Vector3.SignedAngle(transform.forward, directionToPlayer, transform.up);

        if (Mathf.Abs(signedAngleToPlayer) <= createSensor.angle)
        {
            RaycastHit hit;
            // Infinite LOS
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, Mathf.Infinity, ~patrollerController.ignoreLayer, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position, directionToPlayer, Color.green);
                    return true;
                }
            }
        }

        return false;
    }
}