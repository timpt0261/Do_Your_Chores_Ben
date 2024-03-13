using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ConcealmentObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private CinemachineVirtualCamera playerFollow;
    [SerializeField] private CinemachineVirtualCamera closetCamera;

    private void Start()
    {
        closetCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        playerFollow.enabled = true;
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
            playerFollow.enabled = false;
            closetCamera.enabled = true;
        }
        else
        {
            playerFollow.enabled = true;
            closetCamera.enabled = false;
        }
    }
}
