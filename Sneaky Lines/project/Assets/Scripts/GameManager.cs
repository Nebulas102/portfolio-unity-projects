using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject menu;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerDead()
    {   
        Time.timeScale = 0f;

        menu.SetActive(true);
    }

    public void Restart()
    {   
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
