using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMovable
{
    Transform EnemyTransform {get;set; }
    int CurrentWaypoint { get; set; }
    Transform[] WayPoints { get; set; }
    NavMeshAgent agent { get; set; }
    void NextDestination();
    void SetDestination(Vector3 postion = new Vector3(), float speed = 2.0f, bool isChasingPalyer = false);
    void SetPosition(Vector3 postion = new Vector3(), bool useIdle = false);
}
