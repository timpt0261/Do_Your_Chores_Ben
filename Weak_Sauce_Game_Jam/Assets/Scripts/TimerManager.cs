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
        StartEnergizedEffect(timerLength);
    }

    public void StartEnergizedEffect(float duration)
    {
        isActive = true;
        timerBar.SetActive(true);
        timerBar.transform.Find("ProgressBar").GetComponent<TimerProgressBar>().ActivateCountdown(duration);
        StartCoroutine(EndEnergizedEffect(duration));
    }

    IEnumerator EndEnergizedEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Baby's here!");
        StartEnergizedEffect(duration);
    }
}
