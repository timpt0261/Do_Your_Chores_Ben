using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dishes")
        {
            ScoreManager.Instance.addDishesScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dishes")
        {
            ScoreManager.Instance.subtractDishesScore();
        }
    }
}
