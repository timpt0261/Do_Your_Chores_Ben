using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerProgressBar : MonoBehaviour
{
    private bool isActive = false;
    private float currTimer;
    private float maxTimer;

    private Image timerProgress;

    private void Awake()
    {
        timerProgress = GetComponent<Image>();
    }

    private void Update()
    {
        if (isActive)
        {
            currTimer -= Time.deltaTime;
            timerProgress.fillAmount = (currTimer / maxTimer);

            if(currTimer <= 0)
            {
                StopCountdown();
            }
        }
    }

    public void ActivateCountdown(float maxTime)
    {
        isActive = true;
        maxTimer = maxTime;
        currTimer = maxTimer;
    }

    public void StopCountdown()
    {
        isActive = false;
    }
}
