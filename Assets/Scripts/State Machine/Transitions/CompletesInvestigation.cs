using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletesInvestigation : Transition
{
    public bool completeInvestigationDebug = false;
    
    public bool hasCompletedInvestigation = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return hasCompletedInvestigation;
    }

    void transitionAction()
    {
        completeInvestigationDebug = false;
        hasCompletedInvestigation = false;
    }
}
