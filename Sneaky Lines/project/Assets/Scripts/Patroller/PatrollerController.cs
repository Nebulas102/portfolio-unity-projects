using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerController : MonoBehaviour
{
    public LayerMask ignoreLayer;
    public LayerMask occlusionLayers;

    [HideInInspector]
    public GameObject FOVsensor;
}
