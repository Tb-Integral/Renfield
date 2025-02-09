using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingsWindow()
    {
        //some code
    }

    public void Titles()
    {
        //SceneManager.LoadScene("#titles");
    }

    public void Window()
    {
        SceneManager.LoadScene("Window");
    }
}
