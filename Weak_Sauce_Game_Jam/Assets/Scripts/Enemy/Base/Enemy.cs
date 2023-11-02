using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable,ITriggerCheckable
{
    

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    [field: SerializeField] public Transform EnemyTransform { get; set; }
    [field: SerializeField] public int CurrentWaypoint { get; set; } = 0;
    [field: SerializeField] public Transform[] WayPoints { get; set; }
    [field: SerializeField] public NavMeshAgent agent { get; set; }

    public bool IsAgrroed { get; set; }
    public bool IsStriking { get; set; }


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
    public EnemyIdleState EnemyIdleState {get; set; }

    public EnemySearchState EnemySearchState { get; set; }
    public EnemyChaseState EnemyChaseState { get; set; }
    public EnemyAttackState EnemyAttackState { get; set; }
   
    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        
        EnemyIdleState = new EnemyIdleState(this, StateMachine);

        EnemySearchState = new EnemySearchState(this, StateMachine);

        EnemyChaseState = new EnemyChaseState(this, StateMachine);

        EnemyAttackState = new EnemyAttackState(this, StateMachine);
     
    }
    private void Start()
    {
        CurrentHealth = ((IDamagable)this).MaxHealth;
        EnemyTransform = GetComponent<Transform>();
        StateMachine.Initialize(EnemyIdleState);
        pickUpInteraction = GetComponent<PickUpInteraction>();
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void LateUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();

    }

    #region Health / Damage
    public void Damage(float dmageAmount)
    {
        CurrentHealth -= dmageAmount;
        if (CurrentHealth <= 0f)
            Die();
    }

    public void Die()
    {
        // To Do Later

    }

    #endregion

    #region Movement 

    
    public void SetDestination(Vector3 postion = new Vector3() , float speed = 2.0f, bool isChasingPalyer = false)
    {
        if (isChasingPalyer)
        {
            agent.SetDestination(postion);
            agent.speed = speed;
            return;
        }
        agent.SetDestination(WayPoints[CurrentWaypoint].position);
        return;



    }

    public void NextDestination()
    {
        float distance = Vector3.Distance(agent.destination, EnemyTransform.position);
        if (distance <= 1.0f)
        {
            CurrentWaypoint = (CurrentWaypoint + 1) % WayPoints.Length;
            SetDestination();
            return;
        }
    }

    public void SetPosition(Vector3 postion = new Vector3(), bool useIdle=false)
    {
        if (useIdle)
            EnemyTransform.position = _idleTransform.position;
        else
            EnemyTransform.position = postion;
    }
    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType) 
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    
    }
    public enum AnimationTriggerType 
    {
        EnemyChase, 
        EnemySearch,
        EnemyAttack
    }
    #endregion

    #region Distance Checks
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
}
