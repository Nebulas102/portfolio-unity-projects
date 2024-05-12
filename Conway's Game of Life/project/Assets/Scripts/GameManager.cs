using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Generation> generations;

    public static GameManager instance;

    [HideInInspector]
    public Camera camera;

    private void Awake()
    {
        generations = new List<Generation>();

        instance = this;
    }

    private void Start()
    {
        camera = Camera.main;
    }
}
