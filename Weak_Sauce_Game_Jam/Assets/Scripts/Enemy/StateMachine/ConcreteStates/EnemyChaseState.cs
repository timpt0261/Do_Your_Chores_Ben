using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTarget { get; set; }
    [SerializeField] private float _chaseSpeed = 4.5f;
    
public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("In Chase State");
        enemy.SetDestination(_playerTarget.position, _chaseSpeed, true);
        enemy.agent.speed = _chaseSpeed; 
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!enemy.IsAgrroed)
        {
            enemy.StateMachine.ChangeState(enemy.EnemySearchState);
        }

        /*if (enemy.IsStriking)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyAttackState);
        }*/
        _chaseSpeed += .45f * (Time.deltaTime/10);
        enemy.SetDestination(_playerTarget.position, _chaseSpeed, true);



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
