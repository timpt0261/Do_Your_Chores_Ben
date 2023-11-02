using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteraction: MonoBehaviour
{
    [SerializeField] private bool _hide = false;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField]
    private SkinnedMeshRenderer _skinnedMesh;
    public bool Hidden { get { return _hide; } set { _hide = value; } }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

    }
    /* Note: Uncommenting the Character controller will still make the player hide, 
     * but will make it difficult to debug to see is the enemy is able to detect the player to begin with */
   public void HandleHidingPlayer()
    {
        if (_hide)
        {
            /*_characterController.enabled = false;*/
            _animator.enabled = false;
            _skinnedMesh.enabled = false;

        }
        else
        {
            /*_characterController.enabled = true;*/
            _animator.enabled = true;
            _skinnedMesh.enabled = true;

        }
        return;

    }

}
