using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    // Start is called before the first frame update
    void Start()
    {
        executeEntryActions += enterPatrolState;
        // executeStateActions += patrolStateAction;
        executeExitActions += exitPatrolState;
    }

    void enterPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Patrol state");
    }

    void patrolStateAction()
    {
        Debug.Log("[" + gameObject.name + "] is currently in Patrol state");
    }

    void exitPatrolState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Patrol state");
    }
}
