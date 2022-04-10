using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosesPlayer : Transition
{
    public bool losesPlayerDebug = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        if (sm != null)
        {
            return !sm.enemySight.canSeePlayer();
        }

        return false;
    }

    void transitionAction()
    {
        losesPlayerDebug = false;
    }
}
