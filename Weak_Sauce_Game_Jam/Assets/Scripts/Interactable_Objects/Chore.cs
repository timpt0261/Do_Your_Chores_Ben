using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : MonoBehaviour, IInteractable
{
    [TextArea(minLines: 0, maxLines: 1)]
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private Transform transform;


    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interaacting");
        return true;
    }

    private void Update()
    {
        // if picked up follow postion of player
    }
}
