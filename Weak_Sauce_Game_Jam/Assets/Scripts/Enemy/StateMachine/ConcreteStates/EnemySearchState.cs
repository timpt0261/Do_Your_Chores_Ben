using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchState :EnemyState
{
    private float _movementSpeed = 2.0f;
    private float _timeInSearchState = 90;
    private GameObject _playerTarget;
    
    public EnemySearchState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("In Search State");
       enemy.agent.speed = _movementSpeed;
       enemy.CurrentWaypoint = 0;
       enemy.SetDestination();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.agent.speed = 2.0f;
        if (enemy.IsAgrroed)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
        }
        else
        {
           enemy.NextDestination();

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
