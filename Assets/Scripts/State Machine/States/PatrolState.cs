using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public float rotationTime = 1f;
    public float movementSpeed = 5f;
    public float stopThreshold = 0.2f;
    private EnemyMovement movementController = null;

    private 

    // Start is called before the first frame update
    void Start()
    {
        executeEntryActions += enterPatrolState;
        executeStateActions += patrolStateAction;
        executeExitActions += exitPatrolState;

        movementController = gameObject.GetComponent<EnemyMovement>();
        if (movementController == null)
        {
            Debug.LogError("No 'EnemyMovement' script found on [" + gameObject.name + "]");
        }
    }

    void enterPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Patrol state");
        if (movementController != null)
        {
            movementController.setPathToPatrol();
            movementController.speed = movementSpeed;
            movementController.rotationTime = rotationTime;
            movementController.stopThreshold = stopThreshold;
        }
    }

    void patrolStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Patrol state");
        if (movementController != null)
        {
            movementController.delayedMove();
        }
    }

    void exitPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Patrol state");
    }
}
