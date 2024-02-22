using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleJumpScare : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource beatdown;

    [SerializeField] private AudioSource scream;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }
}
