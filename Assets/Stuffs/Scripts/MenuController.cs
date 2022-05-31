using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject mainPanel;
    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnPlay()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}