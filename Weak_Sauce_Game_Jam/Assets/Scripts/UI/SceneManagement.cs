using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement: MonoBehaviour
{
    public void GoToBabyRoom()
    {
        SceneManager.LoadScene("BabyRoom");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void GoToMenuScene() 
    {
        SceneManager.LoadScene("MenuScene");
    
    }

    public void GoToGameOverScene() 
    {
        SceneManager.LoadScene("GameOverScene");
    
    }

    public void ExitGame()
    {
        Debug.Log("Quit the Game.");
        Application.Quit();
    }
}
