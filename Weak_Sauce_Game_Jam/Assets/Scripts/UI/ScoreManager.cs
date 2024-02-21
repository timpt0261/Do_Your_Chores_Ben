using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }
    [SerializeField]
    public int toyScore = 0;
    public int clothesScore = 0;
    public int dishesScore = 0;
    public TextMeshProUGUI scoreText;

    public void addToyScore(int scoreChange = 1)
    {
        toyScore += scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public void subtractToyScore(int scoreChange = 1)
    {
        toyScore -= scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public void addClothesScore(int scoreChange = 1)
    {
        clothesScore += scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public void subtractClothesScore(int scoreChange = 1)
    {
        clothesScore -= scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public void addDishesScore(int scoreChange = 1)
    {
        dishesScore += scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public void subtractDishesScore(int scoreChange = 1)
    {
        dishesScore -= scoreChange;
        scoreText.text = ("Toys put away: " + toyScore + "\nClothes put away: " + clothesScore + "\nDishes put away: " + dishesScore);
    }

    public int getTotalScore()
    {
        return (toyScore + clothesScore + dishesScore);
    }

    public void Update()
    {
        if (getTotalScore() == 10)
            SceneManager.LoadScene("WinningScene");
    }
}
