using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    // Start is called before the first frame update
    void Start()
    {
        executeEntryActions += enterChaseState;
        // executeStateActions += chaseStateAction;
        executeExitActions += exitChaseState;
    }

    void enterChaseState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Chase state");
    }

    void chaseStateAction()
    {
        Debug.Log("[" + gameObject.name + "] is currently in Chase state");
    }

    void exitChaseState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Chase state");
    }
}
