using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] Transform holdArea;
    private GameObject heldObject;
    private Rigidbody heldObjectRigidbody;

    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    // Update is called once per frame
    void Update()
    {
        HandlePickUp();
    }

    void HandlePickUp()
    {
        if (heldObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
        else
        {
            DropObject();
        }
        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
            heldObjectRigidbody.AddForce(moveDirection * pickupForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        Rigidbody pickObjRB = pickObj.GetComponent<Rigidbody>();
        if (pickObjRB)
        {
            heldObjectRigidbody = pickObjRB;
            heldObjectRigidbody.useGravity = false;
            heldObjectRigidbody.drag = 10;
            heldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjectRigidbody.transform.parent = holdArea;
            heldObject = pickObj;
        }
    }

    void DropObject()
    {
        heldObjectRigidbody.useGravity = true;
        heldObjectRigidbody.drag = 1;
        heldObjectRigidbody.constraints = RigidbodyConstraints.None;

        heldObjectRigidbody.transform.parent = null;
        heldObject = null;
    }
}
