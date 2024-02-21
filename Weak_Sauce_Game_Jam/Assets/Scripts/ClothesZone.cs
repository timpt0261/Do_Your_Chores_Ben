using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.addClothesScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.subtractClothesScore();
        }
    }
}
