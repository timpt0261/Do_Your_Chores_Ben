using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && (scoreManger.getTotalScore()) < 10)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
