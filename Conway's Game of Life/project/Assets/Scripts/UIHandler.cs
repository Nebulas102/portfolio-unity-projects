using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pausePlayText;

    private GenerationManager generationManager;

    private void Start()
    {
        generationManager = GenerationManager.instance;
    }

    public void NextGeneration()
    {
        if(!generationManager.autoGeneration)
        {
            generationManager.NextGeneration();
        }
    }

    public void PreviousGeneration()
    {
        if(!generationManager.autoGeneration)
        {
            generationManager.PreviousGeneration();
        }
    }

    public void PlayPauseGeneration()
    {
        if(!generationManager.autoGeneration)
        {
            StartCoroutine(generationManager.ToggleAutoGeneration());
            pausePlayText.text = "Pause";
        }
        else
        {
            StartCoroutine(generationManager.ToggleAutoGeneration());
            StopAllCoroutines();
            pausePlayText.text = "Play";
        }
    }

    public void AllDeadBoard()
    {
        if(!generationManager.autoGeneration)
        {
            generationManager.AllDead();
        }
    }

    public void ClearGenerations()
    {
        if(!generationManager.autoGeneration)
        {
            generationManager.ClearGenerations();
        }
    }
}
