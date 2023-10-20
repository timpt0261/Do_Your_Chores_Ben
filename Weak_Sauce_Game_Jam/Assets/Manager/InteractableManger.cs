using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManger : MonoBehaviour
{
    private GameObject[] interactables;

    private void Start()
    {

        if (interactables == null)
        {
            interactables = GameObject.FindGameObjectsWithTag("Interactable_Object");
        }
    }
}
