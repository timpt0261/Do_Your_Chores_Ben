using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toys")
        {
            ScoreManager.Instance.addScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Toys")
        {
            ScoreManager.Instance.subtractScore();
        }
    }

}
