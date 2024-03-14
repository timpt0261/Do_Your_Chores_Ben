using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toys")
        {
            ScoreManager.Instance.addToyScore();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Toys")
        {
            ScoreManager.Instance.subtractToyScore();
        }
    }

}
