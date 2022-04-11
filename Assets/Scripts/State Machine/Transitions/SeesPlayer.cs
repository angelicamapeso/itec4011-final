using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : Transition
{
    public bool seePlayerDebug = false;

    new void Start()
    {
        base.Start();

        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        if (sm != null && sm.enemySight != null)
        {
            return sm.enemySight.canSeePlayer();
        }

        return false;
        // return seePlayerDebug;
    }
    void transitionAction()
    {
        seePlayerDebug = false;
    }
}
