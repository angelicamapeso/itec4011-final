using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public float rotationTime = 1f;
    public float movementSpeed = 5f;
    public float stopThreshold = 1f;

    public bool hasReachedFirstPatrolPoint = false;
    private Vector2? firstPatrolPoint = null;

    // Start is called before the first frame update
    new void OnEnable()
    {
        base.OnEnable();

        executeEntryActions += enterPatrolState;
        executeStateActions += patrolStateAction;
        executeExitActions += exitPatrolState;
    }

    void enterPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Patrol state");

        if (sm != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.speed = movementSpeed;
            sm.enemyMovement.stopThreshold = stopThreshold;
            sm.enemyMovement.rotationTime = rotationTime;

            firstPatrolPoint = sm.enemyMovement?.patrolPoints?.waypoints?[0]?.position;
            sm.enemyMovement.setDestination((Vector2) firstPatrolPoint, setArrivedAtPatrolPoint);
            sm.enemyMovement.arrivedAtDestinationEvent += setArrivedAtPatrolPoint;
        }
    }

    void patrolStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Patrol state");
        if (sm != null && sm.enemyMovement != null)
        {
            if (!hasReachedFirstPatrolPoint)
            {
                firstPatrolPoint = sm.enemyMovement?.patrolPoints?.waypoints?[0]?.position;
                sm.enemyMovement.setDestination((Vector2)firstPatrolPoint);
                sm.enemyMovement.arrivedAtDestinationEvent += setArrivedAtPatrolPoint;
                sm.enemyMovement.move();
            } else
            {
                sm.enemyMovement.movePatrol();
            }
        }
    }

    void exitPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Patrol state");
        
        if (sm != null && sm.player != null && sm.enemyMovement != null)
        {   
            sm.enemyMovement.arrivedAtDestinationEvent -= setArrivedAtPatrolPoint;
        }

        hasReachedFirstPatrolPoint = false;
    }

    void setArrivedAtPatrolPoint()
    {
        // Debug.Log("Arrived at First Patrol Point: " + firstPatrolPoint);

        hasReachedFirstPatrolPoint = true;
        
        sm.enemyMovement.setPathToPatrol();
        sm.enemyMovement.arrivedAtDestinationEvent -= setArrivedAtPatrolPoint;
    }
}
