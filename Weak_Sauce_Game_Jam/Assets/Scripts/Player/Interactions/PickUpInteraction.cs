using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteraction : MonoBehaviour
{
    [SerializeField] Transform holdArea = null;
    [SerializeField] float holdAreaRadius = 5.0f;
    [ SerializeField] private GameObject heldObject = null;
    private Rigidbody heldObjectRigidbody = null;
    private CharacterController heldObjectController = null;
    [SerializeField] private float pickupForce = 150.0f;

    public bool IsHoldingObject() {
        return heldObject != null;
    }

    public void MoveObject()
    {
        // Debug.Log("Moving object");
        if (heldObjectRigidbody != null)
        {
            if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
            {
                Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
                heldObjectRigidbody.AddForce(moveDirection * pickupForce);
            }
        }
        else if (heldObjectController != null)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
            heldObjectController.Move(moveDirection * Time.deltaTime); // Adjust speed based on Time.deltaTime
        }
    }

    public void PickUpObject(GameObject pickObj)
    {
        Debug.Log("Picking Up object");
        Rigidbody pickObjRB = pickObj.GetComponent<Rigidbody>();
        CharacterController pickObjController = pickObj.GetComponent<CharacterController>();

        if (pickObjRB)
        {
            heldObjectRigidbody = pickObjRB;
            heldObjectRigidbody.useGravity = false;
            heldObjectRigidbody.drag = 10;
            heldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjectRigidbody.transform.parent = holdArea;
            heldObject = pickObj;
        }
        else if (pickObjController)
        {
            heldObjectController = pickObjController;
            heldObjectController.enabled = false; // Disable the CharacterController to 'pick up'
            heldObject = pickObj;
        }
    }

    public void DropObject()
    {
        Debug.Log("Droping Object");
        if (heldObjectRigidbody != null)
        {
            heldObjectRigidbody.useGravity = true;
            heldObjectRigidbody.drag = 1;
            heldObjectRigidbody.constraints = RigidbodyConstraints.None;
            heldObjectRigidbody.transform.parent = null;
            heldObject = null;
        }
        else if (heldObjectController != null)
        {
            heldObjectController.enabled = true; // Re-enable the CharacterController to 'drop'
            heldObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(holdArea.position, holdAreaRadius);
    }
}
