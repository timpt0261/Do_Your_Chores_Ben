using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject playerTarget { get; set; }
    [SerializeField] private HideInteraction _hideInteraction;
    [SerializeField] private Enemy _enemy;
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<Enemy>();
        _hideInteraction = playerTarget.GetComponent<HideInteraction>();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerTarget && !_hideInteraction.Hidden)
        {
            /*Debug.Log("Player Has entered the enemy's striking range seen");*/
            _enemy.SetIsStrikingBool(true);
        }

    }



    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == playerTarget)
        {
            /*Debug.Log("Player has exited the enemy's striking zone");*/
            _enemy.SetIsStrikingBool(false);

        }
    }

}
