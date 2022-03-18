using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosesPlayer : Transition
{
    public bool losesPlayerDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return losesPlayerDebug;
    }

    void transitionAction()
    {
        losesPlayerDebug = false;
    }
}
