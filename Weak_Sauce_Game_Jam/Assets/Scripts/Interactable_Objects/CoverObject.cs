using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public GameObject cameraInCloset;

    public bool Interact(Interactor interactor)
    {
       /* // Enable the event in the interact method
        if (HidingManager.OnHidingStateChanged != null)
        {
            HidingManager.OnHidingStateChanged(cameraInCloset);
        }
*/
        Debug.Log(this.name);
        Debug.Log("Hiding");
        return true;
    }
}
