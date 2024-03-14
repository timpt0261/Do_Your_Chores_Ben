using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Clothes")
        {
            ScoreManager.Instance.addClothesScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Clothes")
        {
            ScoreManager.Instance.subtractClothesScore();
        }
    }
}
