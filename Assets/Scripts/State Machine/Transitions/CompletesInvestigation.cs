using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletesInvestigation : Transition
{
    public bool completeInvestigationDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return completeInvestigationDebug;
    }

    void transitionAction()
    {
        completeInvestigationDebug = false;
    }
}
