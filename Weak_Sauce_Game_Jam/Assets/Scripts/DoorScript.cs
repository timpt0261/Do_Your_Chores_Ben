using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("updating door");
            updateDoor();
        }
    }

    private void updateDoor()
    {
        Debug.Log("setting door bool to: "+ !doorAnimator.GetBool("doorOpen"));
        doorAnimator.SetBool("doorOpen", !doorAnimator.GetBool("doorOpen"));
    }
}
