using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public float rotationTime = 1f;
    public float movementSpeed = 5f;
    public float stopThreshold = 0.2f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); 

        executeEntryActions += enterPatrolState;
        executeStateActions += patrolStateAction;
        executeExitActions += exitPatrolState;
    }

    void enterPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Patrol state");
        Debug.Log(sm.enemyMovement);
        if (sm != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.setPathToPatrol();
            sm.enemyMovement.speed = movementSpeed;
            sm.enemyMovement.stopThreshold = stopThreshold;
            sm.enemyMovement.rotationTime = rotationTime;
        }
    }

    void patrolStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Patrol state");
        if (sm != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.delayedMove();
        }
    }

    void exitPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Patrol state");
    }
}
