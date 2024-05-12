using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 15f;
    
    private InputAction playerControls;
    private Rigidbody rb;

    private Vector2 moveDirection;
    private Vector3 newDirection;

    private void Start()
    {
        playerControls = GetComponent<PlayerInput>().actions["Move"];
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        newDirection = new Vector3(moveDirection.x, 0f, moveDirection.y);
        rb.velocity = newDirection * movementSpeed;
        
        if (newDirection.magnitude == 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (newDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(newDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
