using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
 

    private Enemy _enemy;
    private GameObject _playerTarget;
    private enum States { IDLE, SEARCH, CHASE, ATTACK }
    private States currentState { get; set; }

    #region Idel Lables
    private bool _idleTimerIsRunning = false;
    private float _timeInIdleState; // Initalizes Time the Enemy stays in Idle
    #endregion

    #region Search Lables
    private float _movementSpeed;
    private float _timeInSearchState;
    private bool _searchTimerIsRunning = false;
    
    #endregion

    #region Search Lables 
    private float _chaseSpeed = 4.5f;
    #endregion

    #region Attack Lables
    PickUpInteraction _pickUpInteraction;
    #endregion
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
        Debug.Log("Entering Next State");
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

    public virtual void FrameUpdate()
    {
        if (_searchTimerIsRunning) {
            if (_timeInSearchState > 0)
            {
                _timeInSearchState -= Time.deltaTime;
                DisplayTime(_timeInSearchState);
            }
            else
            {
                Debug.Log("End Of Chase to Idle State");
                _timeInSearchState = 90;
                _searchTimerIsRunning = false;
                
                

            }
        }
        switch (currentState)
        {
            case States.IDLE:
                Debug.Log("Current State Idle");
                HandleIdleFrameUpdate();
                break;
            case States.SEARCH:
                Debug.Log("Current State Search");
                HandleSearhFrameUpdate();
                break;
            case States.CHASE:
                Debug.Log("Current State Chase");
                HandleChaseFrameUpdate();
                break;
            case States.ATTACK:
                break;
            default:
                break;
        }

        
    }


    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
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
                DisplayTime(_timeInIdleState);
            }
            else
            {
                _timeInIdleState = 90;
                _idleTimerIsRunning = false;
                Debug.Log("Change to Search State");
                NextState();

            }

        }
    }

    private void HandleSearhFrameUpdate()
    {
        if (!_searchTimerIsRunning)
        {
            SetState(States.IDLE);
        }
        if (_enemy.IsAgrroed)
        {
           NextState();
        }
        else
        {
            _enemy.NextDestination();

        }
    }

    private void HandleChaseFrameUpdate()
    {
        if (!_searchTimerIsRunning)
        {
            SetState(States.IDLE);
        }
        if (!_enemy.IsAgrroed)
        {
            PreviousState();
        }

        /*if (_enemy.IsStriking)
        {
           this.NextState();
        }*/
        var  _newChaseSpeed = _chaseSpeed + (.45f * (Time.deltaTime / 10));
        _enemy.SetSpeed(_newChaseSpeed);
        _enemy.SetDestination(_playerTarget.transform.position, true);
    }
    #endregion

    private void NextState()
    {
        ExitState();
        switch (currentState)
        {
            case States.IDLE:
                currentState = States.SEARCH;
                break;
            case States.SEARCH:
                currentState = States.CHASE;
                break;
            case States.CHASE:
                currentState = States.ATTACK;
                break;
            case States.ATTACK:

                break;
            default:
                break;
        }
        EnterState();
    }

    private void PreviousState()
    {
        ExitState();
        switch (currentState)
        {
            case States.IDLE:
                break;
            case States.SEARCH:
                currentState = States.IDLE;
                break;
            case States.CHASE:
                currentState = States.SEARCH;
                break;
            case States.ATTACK:
                currentState = States.SEARCH;
                break;
            default:
                break;
        }
        EnterState();
    }
    private void SetState(States newstate) {
        currentState = newstate;
    }
    private void DisplayTime(float timeInIdleState)
    {
        float minutes = Mathf.FloorToInt(timeInIdleState / 60);
        float seconds = Mathf.FloorToInt(timeInIdleState % 60);
        Debug.Log(minutes + ":" + seconds);
    }
}
