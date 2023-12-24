using UnityEngine;

public class Interactor : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs inputs;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Interaction_UI _interaction_Ui = null;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numfound;

    private IInteractable _interactable;
    private GameObject _interactable_Obj;

    [HideInInspector]
    public  HideInteraction _playerHide;
    private PickUpInteraction _playerPickUpInteraction;
    

    private void Start()
    {
        inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        _playerHide = GetComponent<HideInteraction>();
        _playerPickUpInteraction = GetComponent<PickUpInteraction>();
    }


    private void Update()
    {
        _numfound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        bool isPlayerHoldingObject = _playerPickUpInteraction.IsHoldingObject();
        /*Debug.Log("NumFound: " + _numfound + " Is Holding Object: "  + isPlayerHoldingObject );*/

        if (_numfound == 0 && !isPlayerHoldingObject) {
            if (_interactable != null) {
                _interactable = null;
               _interaction_Ui.Close(); 
            }
            return;
        }

        if (_interactable == null && !isPlayerHoldingObject) {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _interactable_Obj = _colliders[0].gameObject;
            if (!_interaction_Ui.IsDisplayed) _interaction_Ui.SetUp(_interactable.InteractionPrompt);

        }

        if (_interactable != null)
        {
            _interactable.Interact(this);
            Debug.Log("Interactable is not null");
            if (inputs.interact)
            {
                if (_interactable_Obj.CompareTag("SafeSpots") && !_playerHide.Hidden) 
                {
                    _playerHide.Hidden = true;
                    _playerHide.HandleHidingPlayer();
                }
                
                if (_interactable_Obj.CompareTag("Toys") && !isPlayerHoldingObject)
                    _playerPickUpInteraction.PickUpObject(_interactable_Obj);
                else
                    _playerPickUpInteraction.MoveObject();
                

            }
            else
            {
                Debug.Log("Off");
                if (_interactable_Obj.CompareTag("SafeSpots") && _playerHide.Hidden)
                {
                    _playerHide.Hidden = false;
                    _playerHide.HandleHidingPlayer();
                }

                if (_interactable_Obj.CompareTag("Toys") && isPlayerHoldingObject)
                    _playerPickUpInteraction.DropObject();

            }
        }

    }



    private void HandleInteractable(IInteractable interactable, GameObject interactable_Obj)
    {
        _interactable.Interact(this);
        bool isPlayerHoldingObject = _playerPickUpInteraction.IsHoldingObject();
        switch (interactable_Obj.tag) {
            case "Toys":
                if (!isPlayerHoldingObject)
                    _playerPickUpInteraction.PickUpObject(interactable_Obj);
                if (isPlayerHoldingObject)
                    _playerPickUpInteraction.MoveObject();
                    break;
            case "SafeSpots":
                _playerHide.Hidden = true;
                _playerHide.HandleHidingPlayer();
                break;
        }
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}