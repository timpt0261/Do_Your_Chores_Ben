using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcealmentObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera closetCamera;

    private void Start()
    {
        closetCamera = GetComponentInChildren<Camera>();
        mainCamera.enabled = true;
        closetCamera.enabled = false;
        
    }


    public bool Interact(Interactor interactor)
    {
        SetUpCoverObj(interactor);
        return true;
    }

    private void SetUpCoverObj(Interactor interactor)
    {
        if (interactor._playerHide.Hidden)
        {
            mainCamera.enabled = false;
            closetCamera.enabled = true;
           
        }
        else
        {
            mainCamera.enabled = true;
            closetCamera.enabled = false;
           

        }
    }

   
}
