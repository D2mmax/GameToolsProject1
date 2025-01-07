using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool controlsOpen = false;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevelTutorial()
    {
       SceneManager.LoadScene(1);
    }
    public void LoadLevel1()
    {
       SceneManager.LoadScene(2);
    }
    public void LoadLevel2()
    {
       SceneManager.LoadScene(3);
    }
    public void LoadLevel3()
    {
       SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsToggle()
    {

        mainMenuPanel.GetComponent<Canvas> ().enabled = false;
        controlsPanel.GetComponent<Canvas> ().enabled = true;
    }
}
