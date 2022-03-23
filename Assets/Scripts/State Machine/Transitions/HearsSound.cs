using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearsSound : Transition
{
    public bool hearPlayerDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return hearPlayerDebug;
    }

    void transitionAction()
    {
        hearPlayerDebug = false;
    }
}
