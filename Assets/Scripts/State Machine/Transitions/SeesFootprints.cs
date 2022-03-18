using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesFootprints : Transition
{
    public bool seeFootprintDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return seeFootprintDebug;
    }

    void transitionAction()
    {
        seeFootprintDebug = false;
    }
}
