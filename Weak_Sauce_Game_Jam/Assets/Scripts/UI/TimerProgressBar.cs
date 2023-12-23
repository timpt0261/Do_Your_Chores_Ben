using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerProgressBar : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField] private float currTimer = 120;
    [SerializeField] private float maxTimer = 120;

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
            float currFill = currTimer / maxTimer;
            if (currFill < 0)
            {
                currFill = 0;
            }
            timerProgress.fillAmount = currFill;
            if(currFill != 0)
            {
                if (currFill <= 0.33)
                {
                    Debug.Log(" In Phase 3");
                }
                else if (currFill <= 0.66)
                {
                    Debug.Log(" In Phase 2");
                }
                else
                {
                    Debug.Log(" In Phase 1");
                }
            }else
            {
                StopCountdown();
                //Debug.Log("over");
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
