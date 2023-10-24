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

    [SerializeField] private Transform _grapPoint;

    private bool interact;

    public bool Interact { get { return interact; } }

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numfound;

    private IInteractable _interactable;

    private Transform _interactable_transform;

    // Callback Event that takes mapped inoput for input
    public void OnInteract(InputAction.CallbackContext value)
    {
        InteractInput(value.action.triggered);
        Debug.Log(interact);
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
            _interactable_transform = _colliders[0].GetComponent<Transform>();

            if (_interactable != null)
            {
                if (!_interaction_Ui.IsDisplayed) _interaction_Ui.SetUp(_interactable.InteractionPrompt);

                if (interact)
                {
                    _interactable.Interact(this);
                    _interactable_transform.position = _grapPoint.position;
                }
            }

        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interaction_Ui.IsDisplayed) _interaction_Ui.Close();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_grapPoint.position, _interactionPointRadius);
    }
}