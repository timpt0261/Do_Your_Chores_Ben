using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMovable
{
    int CurrentWaypoint { get; set; }
    Transform[] WayPoints { get; set; }
    NavMeshAgent agent { get; set; }

    void SetSpeed(float newSpeed);
    void NextDestination();
    void SetDestination(Vector3 postion = new Vector3(), bool isChasingPalyer = false);
    void SetPosition(Vector3 postion = new Vector3(), bool useIdle = false);
}
