using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameplays()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
