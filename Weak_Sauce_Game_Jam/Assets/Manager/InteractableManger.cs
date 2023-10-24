using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _interactables;

    private void Awake()
    {

        if (_interactables == null)
        {
            _interactables = GameObject.FindGameObjectsWithTag("Interactable");
        }
    }
}
