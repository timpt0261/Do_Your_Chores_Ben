using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteraction: MonoBehaviour
{
    [SerializeField] private bool _hide = false;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField]
    private SkinnedMeshRenderer skinnedMesh;
    public bool Hidden { get { return _hide; } set { _hide = value; } }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

    }
   public void HandleHidingPlayer()
    {
        if (_hide)
        {
            _characterController.enabled = false;
            _animator.enabled = false;
            skinnedMesh.enabled = false;

        }
        else
        {
            _characterController.enabled = true;
            _animator.enabled = true;
            skinnedMesh.enabled = true;

        }
        return;

    }

}
