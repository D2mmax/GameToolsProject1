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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsToggle()
    {

        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}
