using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager playerManager;

    private Renderer renderer;
    private Camera camera;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        camera = Camera.main;
    }

    private void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z);
    }
}
