using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;

    public Material playerBaseMaterial;
    public Material playerFOVMaterial;
    public Material playerLOSMaterial;
    public Material playerDeadMaterial;

    private Renderer renderer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        renderer = player.GetComponent<Renderer>();
    }

    public void playerInFOV()
    {
        renderer.material = playerFOVMaterial;
    }

    public void playerInLOS()
    {
        renderer.material = playerLOSMaterial;
    }

    public void playerSafe()
    {
        renderer.material = playerBaseMaterial;
    }

    public void GameOver()
    {   
        renderer.material = playerDeadMaterial;
        GameManager.instance.PlayerDead();
    }
}
