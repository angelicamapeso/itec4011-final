using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : Transition
{
    public bool seePlayerDebug = false;

    private GameObject player = null;

    private void Start()
    {
        executeTransitionActions += transitionAction;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No player is present in the scene!");
        }
    }

    public override bool isTriggered()
    {
        return canSeePlayer();
        // return seePlayerDebug;
    }

    bool canSeePlayer()
    {
        return (player != null && isPlayerInFront() && !isPlayerBlocked());
    }

    bool isPlayerInFront()
    {
        Vector2 forward = transform.TransformDirection(Vector2.up);
        Vector2 toPlayer = player.transform.position - transform.position;

        if (Vector2.Dot(forward, toPlayer) > 0 && Vector2.Angle(forward, toPlayer) < 45)
        {
            return true;
        }

        return false;
    }

    bool isPlayerBlocked()
    {
        Vector2 toPlayer = (player.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayer, Mathf.Infinity, LayerMask.GetMask("Collidable", "Player"));

        if (hit.collider != null)
        {
            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y);

            if (hit.collider.tag != "Player")
            {
                Debug.DrawRay(transform.position, hitPoint - transform.position, Color.blue);
                return true;
            } else
            {
                Debug.DrawRay(transform.position, hitPoint - transform.position, Color.red);
                return false;
            }
            
        }

        return true;
    }

    void transitionAction()
    {
        seePlayerDebug = false;
    }
}
