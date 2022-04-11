using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesFootprints : Transition
{
    public GameObject closestVisibleFootprint = null;
    public bool seeFootprintDebug = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        if (sm != null && sm.enemySight != null)
        {
            closestVisibleFootprint = sm.enemySight.getClosestVisibleFootprint(sm.lastFootPrints);

            if (closestVisibleFootprint != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        return false;
    }

    void transitionAction()
    {
        seeFootprintDebug = false;

        if (closestVisibleFootprint != null)
        {
            sm.lastFootPrints.Add(closestVisibleFootprint.GetInstanceID());
            sm.lastInterestPoint = closestVisibleFootprint.transform.position;
        }
    }
}
