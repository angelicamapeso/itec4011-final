using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State
{
    public float rotationTime = 2.5f;
    public float movementSpeed = 3.5f;
    public float stopThreshold = 1f;

    private bool hasReachedInvestigationPoint = false;

    private bool isLookingAround = false;
    private float[] lookAroundAngles = null;
    public int lookAroundAngleCount = 0;
    
    private CompletesInvestigation completesInvestigationTransition = null;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        completesInvestigationTransition = gameObject.GetComponent<CompletesInvestigation>();
        if (completesInvestigationTransition == null)
        {
            Debug.LogError("No 'CompletesInvestigation' transition exists on [" + gameObject.name + "]");
        }
        
        executeEntryActions += enterInvestigateState;
        executeStateActions += investigateStateAction;
        executeExitActions += exitInvestigateState;
    }

    void enterInvestigateState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Investigate state");

        if (sm != null && sm.enemyMovement != null && sm.enemyRotation != null)
        {
            sm.enemyMovement.speed = movementSpeed;
            sm.enemyMovement.stopThreshold = stopThreshold;
            sm.enemyMovement.rotationTime = rotationTime;

            sm.SetLastInterestPoint();
            sm.enemyMovement.setDestination((Vector2) sm.lastInterestPoint, stopMovingToPoint);
            sm.enemyMovement.arrivedAtDestinationEvent += stopMovingToPoint;
        }
    }

    void investigateStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Investigate state");
        if (!hasReachedInvestigationPoint && sm != null && sm.enemyMovement != null)
        {
            sm.enemyMovement.move();
        } else if (hasReachedInvestigationPoint && !isLookingAround)
        {
            triggerLookAround();
        } else if (isLookingAround)
        {
            lookAround();
        }
    }

    void exitInvestigateState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Investigate state");

        resetState();
    }

    void resetState()
    {
        if (sm != null && sm.enemyMovement != null && sm.enemyRotation != null)
        {
            sm.enemyMovement.arrivedAtDestinationEvent -= stopMovingToPoint;
            sm.enemyRotation.arrivedAtRotation -= increaseLookAroundAngleCount;
        }

        hasReachedInvestigationPoint = false;
        
        isLookingAround = false;
        
        lookAroundAngles = null;
        lookAroundAngleCount = 0;
    }

    void stopMovingToPoint()
    {
        hasReachedInvestigationPoint = true;
        sm.enemyMovement.arrivedAtDestinationEvent -= stopMovingToPoint;
    }

    void triggerLookAround()
    {
        if (sm != null && sm.enemyRotation != null)
        {
            float[] angles = new float[2];

            float currentOrientation = sm.enemyRotation.getCurrentOrientation();
            angles[0] = currentOrientation - 90;
            angles[1] = currentOrientation + 90;

            lookAroundAngles = angles;

            sm.enemyRotation.rotateTowards(lookAroundAngles[0]);
            sm.enemyRotation.arrivedAtRotation += increaseLookAroundAngleCount;

            isLookingAround = true;
        }
    }

    void lookAround()
    {
        if (sm != null && sm.enemyRotation != null)
        {
            sm.enemyRotation.rotateTowards(lookAroundAngles[lookAroundAngleCount]);
        }
    }

    void increaseLookAroundAngleCount()
    {
        lookAroundAngleCount += 1;

        if (lookAroundAngleCount > 1)
        {
            if (completesInvestigationTransition != null)
            {
                completesInvestigationTransition.hasCompletedInvestigation = true;
            }
        }
    }
}
