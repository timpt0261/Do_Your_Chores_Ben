using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class TimerProgressBar : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField] private float currTimer = 120;
    [SerializeField] private float maxTimer = 120;
    [SerializeField] private TextMeshProUGUI notifcation;

    [SerializeField] private Animator animator;

    private bool cutscenePlayed = false;
    public GameManager manager;

    private Image timerProgress;

    private void Awake()
    {
        timerProgress = GetComponent<Image>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            currTimer -= Time.deltaTime;
            float currFill = currTimer / maxTimer;
            if (currFill < 0)
            {
                currFill = 0;
            }
            timerProgress.fillAmount = currFill;
            if (currFill != 0)
            {
                Debug.Log(UpdateText(currTimer));
                notifcation.text = UpdateText(currTimer);

            }
            else
            {
                StopCountdown();
                //Debug.Log("over");
            }
        }
        if (!cutscenePlayed)
        {
            manager.StartState();
            cutscenePlayed = true;
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
        SceneManager.LoadScene("GameOverScene");
    }

    private string UpdateText(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        return minutes + ":" + seconds + "\n Left To Finish Your Chores";
    }
}
