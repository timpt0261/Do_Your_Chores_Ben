using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStateMachine
{ 
    private Enemy _enemy;
    private GameObject _playerTarget;
    private enum States { IDLE, SEARCH, CHASE, ATTACK }
    private States currentState { get; set; }

    // Idle Lables
    private bool _idleTimerIsRunning = false;
    private float _timeInIdleState; // Initalizes Time the Enemy stays in Idle

    // Search Lables
    private float _movementSpeed;
    private float _timeInSearchState;
    private bool _searchTimerIsRunning = false;

     // Search Lables 
    private float _chaseSpeed = 4.5f;

    // Attack Lables
    PickUpInteraction _pickUpInteraction;

    public EnemyStateMachine(Enemy enemy)
    {
        _enemy = enemy;
        _playerTarget = GameObject.FindGameObjectWithTag("Player");
        
        _movementSpeed = enemy.movementSpeed;
        _timeInIdleState = enemy.timeInIdleState;

        _timeInSearchState = enemy.timeinSearchState;
        _chaseSpeed = enemy.chaseSpeed;

        _pickUpInteraction = enemy.pickUpInteraction;
    }

    #region State Manipulation

    public void Initialize() {
        currentState = States.IDLE;
        EnterState();
    }
    public void EnterState()
    {
        /*Debug.Log("Entering Next State");*/
        switch (currentState)
        {
            case States.IDLE:
                _timeInIdleState = _enemy.timeInIdleState;
                _idleTimerIsRunning = true;
                _enemy.SetPosition(default, true);
                break;
            case States.SEARCH:
                _enemy.SetSpeed(_movementSpeed);
                _enemy.CurrentWaypoint = 0;
                _enemy.SetDestination();
                _searchTimerIsRunning = true;
                break;
            case States.CHASE:
                _enemy.SetSpeed(_chaseSpeed);
                _enemy.SetDestination(_playerTarget.transform.position, true);
                break;
            case States.ATTACK:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            default:
                break;
        }


    }

    public void ExitState()
    {
        Debug.Log("Entering Exit State");
        switch (currentState)
        {
            case States.IDLE:
                break;
            case States.SEARCH:
                break;
            case States.CHASE:
                _enemy.SetSpeed(_movementSpeed);
                break;
            case States.ATTACK:
                break;
            default:
                break;
        }
    }

    public void FrameUpdate()
    {
        switch (currentState)
        {
            case States.IDLE:
               /* Debug.Log("Current State Idle");*/
                HandleIdleFrameUpdate();
                break;
            case States.SEARCH:
/*                Debug.Log("Current State Search");*/
                HandleSearchFrameUpdate();
                break;
            case States.CHASE:
/*                Debug.Log("Current State Chase");*/
                HandleChaseFrameUpdate();
                break;
            case States.ATTACK:
/*                Debug.Log("Game Over");*/
    
                break;
            default:
                break;
        }

        
    }


    public  void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {


    }

    #endregion

    #region HandleFrameUpdate Methods
    private void HandleIdleFrameUpdate()
    {
        if (_idleTimerIsRunning)
        {
            if (_timeInIdleState > 0)
            {
                _timeInIdleState -= Time.deltaTime;
                /*DisplayTime(_timeInIdleState);*/
            }
            else
            {
                _timeInIdleState = 90;
                _idleTimerIsRunning = false;
                Debug.Log("Change to Search State");
                ChangeState(States.SEARCH);

            }

        }
    }

    private void HandleSearchFrameUpdate()
    {
        if (_enemy.IsAgrroed)
        {
           ChangeState(States.CHASE);
        }

        _enemy.NextDestination();
    }

    private void HandleChaseFrameUpdate()
    {
        
        if (!_enemy.IsAgrroed)
        {
            ChangeState(States.SEARCH);
        }

        if (_enemy.IsStriking)
        {
            this.ChangeState(States.ATTACK);
        }
        var  _newChaseSpeed = _chaseSpeed + (.45f * (Time.deltaTime / 10));
        _enemy.SetSpeed(_newChaseSpeed);
        _enemy.SetDestination(_playerTarget.transform.position, true);
    }
    #endregion
     
   private void ChangeState(States newState){
     ExitState();
     currentState = newState;
     EnterState();
   }
  
    private void DisplayTime(float timeInIdleState)
    {
        float minutes = Mathf.FloorToInt(timeInIdleState / 60);
        float seconds = Mathf.FloorToInt(timeInIdleState % 60);
        Debug.Log(minutes + ":" + seconds);
    }
}
