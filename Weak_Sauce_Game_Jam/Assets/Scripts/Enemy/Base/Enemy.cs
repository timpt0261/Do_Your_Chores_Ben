using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemyMovable, ITriggerCheckable
{

    [field: SerializeField] public int CurrentWaypoint { get; set; } = 0;
    [field: SerializeField] public Transform[] WayPoints { get; set; }
    [field: SerializeField] public NavMeshAgent agent { get; set; }

    public bool IsAgrroed { get; set; }
    public bool IsStriking { get; set; }

    [SerializeField] private TimerManager timerManager;
    #region Idle Label
    public float timeInIdleState = 90; // in seconds
    [SerializeField] private Transform _idleTransform;

    #endregion
    #region Search
    public float timeinSearchState = 120;
    public float movementSpeed = 2.0f;
    #endregion

    #region Chase
    public float chaseSpeed = 3.0f;
    #endregion

    #region Attack
    public PickUpInteraction pickUpInteraction;

    #endregion

    #region State Machine
    public EnemyStateMachine StateMachine { get; set; }
    #endregion

    [SerializeField] private AudioSource _enemyAudio;
    private void Awake()
    {
        _enemyAudio = GetComponent<AudioSource>();
        _enemyAudio.volume = 0.0f;
        StateMachine = new EnemyStateMachine(this);

    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StateMachine.Initialize();
        pickUpInteraction = GetComponent<PickUpInteraction>();
    }


    private void Update()
    {
        StateMachine.FrameUpdate();
    }

    #region Movement 

    public void SetSpeed(float newSpeed)
    {
        Debug.Log("Setting Speed to " + newSpeed);
        agent.speed = newSpeed;
    }

    public void SetDestination(Vector3 postion = new Vector3(), bool isChasingPalyer = false)
    {
        if (isChasingPalyer)
        {
            agent.SetDestination(postion);
            return;
        }
        agent.SetDestination(WayPoints[CurrentWaypoint].position);
        return;
    }

    public void NextDestination()
    {
        float distance = Vector3.Distance(agent.destination, this.transform.position);
        if (distance <= 1.0f)
        {
            CurrentWaypoint = (CurrentWaypoint + 1) % WayPoints.Length;
            SetDestination();
            return;
        }
    }

    public void SetPosition(Vector3 postion = new Vector3(), bool useIdle = false)
    {
        if (useIdle)
            this.transform.position = _idleTransform.position;
        else
            this.transform.position = postion;
    }
    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.AnimationTriggerEvent(triggerType);

    }
    public enum AnimationTriggerType
    {
        EnemyChase,
        EnemySearch,
        EnemyAttack
    }
    #endregion

    #region Trigger Checks
    public void SetAggroStatus(bool isAgrroed)
    {
        Debug.Log("IsAgrroed: " + IsAgrroed);
        IsAgrroed = isAgrroed;

    }

    public void SetIsStrikingBool(bool isStriking)
    {
        Debug.Log("IsStriking: " + IsStriking);
        IsStriking = isStriking;
    }
    #endregion

    #region Audio

    public void PlayAudio(){
        _enemyAudio.Play();
    }
    public void SetVolume(float volume)
    {
        _enemyAudio.volume = Mathf.Lerp(0.0f, 1.0f, volume);
    }

    #endregion
}
