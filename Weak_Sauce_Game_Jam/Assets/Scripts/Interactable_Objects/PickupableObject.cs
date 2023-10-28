using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour, IInteractable
{
    [TextArea(minLines: 0, maxLines: 1)]
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        //Debug.Log(this.name);
        //Debug.Log("Interacting");
        return true;
    }
}
