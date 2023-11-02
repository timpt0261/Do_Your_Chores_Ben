using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private bool _timerIsRunning = false;
    private float _timeInIdleState; // Initalizes Time the Enemy stays in Idle
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
       enemy.SetPosition(default,true);
        Debug.Log("Entering Idle State");
        _timeInIdleState = enemy.timeInIdleState;
        _timerIsRunning = true;
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (_timerIsRunning) {
            if (_timeInIdleState > 0)
            {
                _timeInIdleState -= Time.deltaTime;
                DisplayTime(_timeInIdleState);
            }
            else
            {
                _timeInIdleState = 0;
                _timerIsRunning = false;
                Debug.Log("Change to Search State");
                enemy.StateMachine.ChangeState(enemy.EnemySearchState);
                
            }

        }
        
    }

    private void DisplayTime(float timeInIdleState)
    {
        float minutes = Mathf.FloorToInt(timeInIdleState / 60);
        float seconds = Mathf.FloorToInt(timeInIdleState % 60);
        Debug.Log(minutes + ":" + seconds);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
