using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject closet;

    public delegate void HideAction(GameObject hidingObject);
    public static event HideAction OnHidingStateChanged;

    private void Update()
    {
        // Check if the player is in the closet or any other hiding object
        if (IsPlayerHidden(closet))
        {
            // Player is hidden in the closet
            NotifyHidingStateChanged(closet);
        }
        else
        {
            // Player is not hidden
            NotifyHidingStateChanged(null);
        }
    }

    private bool IsPlayerHidden(GameObject hidingObject)
    {
        if (hidingObject == null || player == null)
        {
            return false;
        }

        // You can implement your own logic to determine if the player is hidden in the hidingObject.
        // For example, you can check if the player's position is inside the bounds of the hidingObject.
        // Replace the condition below with your actual hiding logic.
        return player.transform.position.x > hidingObject.transform.position.x;
    }

    private void NotifyHidingStateChanged(GameObject hidingObject)
    {
        if (OnHidingStateChanged != null)
        {
            OnHidingStateChanged(hidingObject);
        }
    }
}
