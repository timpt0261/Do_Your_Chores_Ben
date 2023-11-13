using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player is in the objective zone");
        } else if (other.gameObject.tag == "Interactable")
        {
            ScoreManager.Instance.addScore();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player is in the objective zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player has left the objective zone");
        }
        else if (other.gameObject.tag == "Interactable")
        {
            ScoreManager.Instance.subtractScore();
        }
    }

}
