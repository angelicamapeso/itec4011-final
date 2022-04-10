using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearsSound : Transition
{
    public bool hearPlayerDebug = false;
    private Main playerScript = null;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        playerScript = sm.player.GetComponent<Main>();
        if (playerScript == null)
        {
            Debug.LogError("'Main' script not found on Player");
        }

        executeTransitionActions += transitionAction;
    }

    public override bool isTriggered()
    {
        return canHearPlayer();
    }

    void transitionAction()
    {
        hearPlayerDebug = false;
    }

    bool canHearPlayer()
    {
        if (sm.player != null && playerScript != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, sm.player.transform.position);
            if (distanceToPlayer <= playerScript.soundRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
