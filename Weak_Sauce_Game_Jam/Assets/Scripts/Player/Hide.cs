using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private bool isHidden = false;
    private CharacterController characterController;
    private Renderer playerRenderer;

    private void Start()
    {
        isHidden = false;
        characterController = GetComponent <CharacterController>();
        playerRenderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        HidingManager.OnHidingStateChanged += HidePlayer;
    }

    void OnDisable()
    {
        HidingManager.OnHidingStateChanged -= HidePlayer;
    }

    private void HidePlayer(GameObject camera)
    {
        if (isHidden)
        {
            // Make the player visible
            playerRenderer.enabled = true;

            // Allow the player to move
            characterController.enabled = true;
        }
        else
        {
            // Make the player invisible
            playerRenderer.enabled = false;

            // Prevent the player from moving
            characterController.enabled = false;

            // Switch to the camera in the closet
            Camera.main.enabled = false;
            camera.GetComponent<Camera>().enabled = true;
        }

        isHidden = !isHidden;
    }
}
