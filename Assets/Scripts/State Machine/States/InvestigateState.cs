using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State
{
    // Start is called before the first frame update
    void Start()
    {
        executeEntryActions += enterInvestigateState;
        // executeStateActions += investigateStateAction;
        executeExitActions += exitInvestigateState;
    }

    void enterInvestigateState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Investigate state");
    }

    void investigateStateAction()
    {
        Debug.Log("[" + gameObject.name + "] is currently in Investigate state");
    }

    void exitInvestigateState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Investigate state");
    }
}
