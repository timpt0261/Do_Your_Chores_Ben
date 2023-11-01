using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameObject timerBar;
    public bool isActive;
    public int timerLength = 10;

    private void Awake()
    {
        StartTimerEffect(timerLength);
    }

    public void StartTimerEffect(float duration)
    {
        isActive = true;
        timerBar.SetActive(true);
        timerBar.transform.Find("ProgressBar").GetComponent<TimerProgressBar>().ActivateCountdown(duration);
        //StartCoroutine(EndTimerEffect(duration));
    }

    /*
    IEnumerator EndTimerEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Timer ended");
        //StartTimerEffect(duration);
    }
    */
}
