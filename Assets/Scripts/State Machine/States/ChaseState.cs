using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public float rotationTime = 0.1f;
    public float movementSpeed = 4f;
    public float stopThreshold = 1f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        executeEntryActions += enterChaseState;
        executeStateActions += chaseStateAction;
        executeExitActions += exitChaseState;
    }

    void enterChaseState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Chase state");
        if (sm != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.speed = movementSpeed;
            sm.enemyMovement.stopThreshold = stopThreshold;
            sm.enemyMovement.rotationTime = rotationTime;
        }
    }

    void chaseStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Chase state");
        if (sm != null && sm.player != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.setDestination(sm.player.transform.position);
            sm.enemyMovement.move();
        }
    }

    void exitChaseState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Chase state");
    }
}
