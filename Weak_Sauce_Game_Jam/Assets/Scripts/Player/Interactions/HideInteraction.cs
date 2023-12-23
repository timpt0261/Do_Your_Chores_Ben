using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class HideInteraction : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs inputs;
    [SerializeField] private bool _hide = false;
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    private Animator _animator;
    private PlayerInput _playerInput;
    private Canvas _canvas;

    private const string playerActionMap = "Player";
    private const string movementActionName = "Move";

    public bool Hidden
    {
        get => _hide;
        set
        {
            _hide = value;
            HandleHidingPlayer();
        }
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
    }



    public void HandleHidingPlayer()
    {
        SetPlayerMovement(!_hide);
        _animator.enabled = !_hide;
        _skinnedMesh.enabled = !_hide;
        _canvas.enabled = !_hide;
    }

    private void SetPlayerMovement(bool enableMovement)
    {
        InputActionMap actionMap = _playerInput.actions.FindActionMap(playerActionMap);

        if (actionMap == null)
        {
            Debug.LogWarning("Action Map 'Player' not found.");
            return;
        }

        InputAction movementAction = actionMap.FindAction(movementActionName);

        if (movementAction == null)
        {
            Debug.LogWarning("Movement action not found.");
            return;
        }

        if (enableMovement)
        {
            movementAction.Enable();
        }
        else
        {
            movementAction.Disable();
        }
    }
}
