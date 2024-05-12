using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float speed = 15f;
    [SerializeField] 
    private float restTime;
    [SerializeField]
    private float rotationSpeed = 15f;
    [SerializeField] 
    private GameObject[] route;

    private int routeIndex = 0;
    private bool isResting = false;

    void Update()
    {   
        Vector3 newRoute = new Vector3(route[routeIndex].transform.position.x, transform.position.y, route[routeIndex].transform.position.z);

         Vector3 lookRoute = new Vector3(newRoute.x - transform.position.x, 0f, newRoute.z - transform.position.z);


        if (transform.position != newRoute)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookRoute);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, newRoute, speed * Time.deltaTime);
        }
        else
        {
            if (!isResting)
            {
                StartCoroutine(RestAtPoint());
            }
        }
    }

    IEnumerator RestAtPoint()
    {
        isResting = true;

        yield return new WaitForSeconds(restTime);

        if (routeIndex == route.Length - 1)
        {
            routeIndex = 0;
        }
        else
        {
            routeIndex++;
        }

        isResting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < route.Length; i++)
        {
            Vector3 start = route[i].transform.position;
            Vector3 destination = i < route.Length - 1 ? route[i + 1].transform.position : route[0].transform.position;
            Gizmos.DrawLine(start, destination);
        }
    }
}
