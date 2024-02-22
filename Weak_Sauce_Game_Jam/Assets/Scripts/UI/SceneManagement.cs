using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator animator;

    public void mainFade()
    {
        animator.SetTrigger("FadeOut");
    }

    public void creditsFade()
    {
        animator.SetTrigger("FadeOutCredits");
    }

    public void menuFade()
    {
        animator.SetTrigger("FadeOutMenu");
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
