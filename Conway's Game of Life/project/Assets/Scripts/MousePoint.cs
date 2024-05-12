using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MousePoint : MonoBehaviour
{
    [HideInInspector]
    public GameObject selectedTile;
    [HideInInspector]
    public GameObject hoveredTile;

    [HideInInspector]
    public Vector3 mousePosition;

    public RaycastHit hit;
    
    private PlayerInput playerInput;
    public InputAction toggleTileAction;

    public static MousePoint instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();

        toggleTileAction = playerInput.actions["ToggleTile"];
    }

    private void Update()
    {
        if(toggleTileAction.triggered && !GenerationManager.instance.autoGeneration)
        {
            if(hit.transform.GetComponent<Tile>())
            {
                selectedTile = hit.transform.gameObject;
                selectedTile.GetComponent<Tile>().ToggleTileState();
            }
        }
    }

    private void FixedUpdate()
    {   
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = GameManager.instance.camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.transform.CompareTag("Tile"))
                {
                    mousePosition = hit.point;
                    hoveredTile = hit.transform.gameObject;
                }
            }
        }
    }
}
