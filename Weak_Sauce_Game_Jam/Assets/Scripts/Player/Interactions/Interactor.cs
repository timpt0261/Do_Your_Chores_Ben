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
    public HideInteraction playerHide;
    private PickUpInteraction _playerPickUpInteraction;
    

    private void Start()
    {
        inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        playerHide = GetComponent<HideInteraction>();
        _playerPickUpInteraction = GetComponent<PickUpInteraction>();
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

                if (inputs.interact)
                {
                    HandleInteractable(_interactable, _interactable_Obj);
                }
                else
                {
                    playerHide.Hidden = false;
                    _interactable.Interact(this);
                }

            }

        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interaction_Ui.IsDisplayed) _interaction_Ui.Close();
        }

        playerHide.HandleHidingPlayer();
        _playerPickUpInteraction.HandlePickUp();

    }

    private void HandleInteractable(IInteractable interactable, GameObject interactable_Obj)
    {
        _interactable.Interact(this);
        switch (interactable_Obj.tag) {
            case "Toys":
                _playerPickUpInteraction.HandlePickUp(interactable_Obj);
                break;
            case "SafeSpots":
                playerHide.Hidden = true;
                playerHide.HandleHidingPlayer();
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