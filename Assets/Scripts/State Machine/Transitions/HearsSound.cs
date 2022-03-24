using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearsSound : Transition
{
    public bool hearPlayerDebug = false;
    private GameObject player = null;
    private Main playerScript = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Main script = player.GetComponent<Main>();
            if (script != null)
            {
                playerScript = script;
            } else
            {
                Debug.LogError("'Main' script not found on Player.");
            }
        } else
        {
            Debug.LogError("No player found in scene.");
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
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
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
