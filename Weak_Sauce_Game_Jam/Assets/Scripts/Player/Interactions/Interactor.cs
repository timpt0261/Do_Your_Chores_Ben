using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private InputAction actions;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Interaction_UI _interaction_Ui;

    public bool interact;

    public bool Interact { get { return interact; } }

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numfound;

    private IInteractable _interactable;
    private GameObject _interactable_Obj;

    [SerializeField] Transform holdArea;
    private GameObject heldObject = null;
    private Rigidbody heldObjectRigidbody = null;

    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    // Callback Event that takes mapped inoput for input
    public void OnInteract(InputAction.CallbackContext value)
    {
        InteractInput(value.action.triggered);
/*        Debug.Log(interact);*/
    }

    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }
    private void Update()
    {
        _numfound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numfound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _interactable_Obj = _colliders[0].gameObject;

            if (_interactable != null)
            {
                if (!_interaction_Ui.IsDisplayed) _interaction_Ui.SetUp(_interactable.InteractionPrompt);
                Debug.Log(interact);
                if (interact)
                {
                    _interactable.Interact(this);
                    HandlePickUp(_interactable_Obj);
                }
 
            }

        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interaction_Ui.IsDisplayed) _interaction_Ui.Close();
        }

        HandlePickUp(heldObject);
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(holdArea.position, _interactionPointRadius);
    }



    public void HandlePickUp(GameObject pickObj)
    {
        //Debug.Log("Handle Pick Up called");
        if (heldObject == null)
        {
            PickUpObject(pickObj);
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

    private void MoveObject()
    {
        Debug.Log("Move Object called");
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
            heldObjectRigidbody.AddForce(moveDirection * pickupForce);
        }
    }

    private void PickUpObject(GameObject pickObj)
    {
        Debug.Log(pickObj.name);
        Debug.Log("Pick Up Object called");
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

    private void DropObject()
    {
        heldObjectRigidbody.useGravity = true;
        heldObjectRigidbody.drag = 1;
        heldObjectRigidbody.constraints = RigidbodyConstraints.None;
        heldObjectRigidbody.transform.parent = null;
        heldObject = null;
    }

}