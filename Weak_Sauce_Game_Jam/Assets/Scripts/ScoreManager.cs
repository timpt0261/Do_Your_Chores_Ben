using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Score Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        scoreText.text = ("Score: " + playerScore);
    }
    [SerializeField]
    private int playerScore = 0;
    public TextMeshProUGUI scoreText;

    public void addScore(int scoreChange = 1)
    {
        playerScore += scoreChange;
        scoreText.text = ("Score: " + playerScore);
    }

    public void subtractScore(int scoreChange = 1)
    {
        playerScore -= scoreChange;
        scoreText.text = ("Score: " + playerScore);
    }
}
