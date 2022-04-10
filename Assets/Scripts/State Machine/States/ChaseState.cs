using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public float rotationTime = 0.1f;
    public float movementSpeed = 4f;
    public float stopThreshold = 1f;
    private GameObject player = null;
    private EnemyMovement movementController = null;
    public Vector2? lastSeenPlayerPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        executeEntryActions += enterChaseState;
        executeStateActions += chaseStateAction;
        executeExitActions += exitChaseState;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No player in scene!");
        }

        movementController = gameObject.GetComponent<EnemyMovement>();
        if (movementController == null)
        {
            Debug.LogError("No 'EnemyMovement' script found on [" + gameObject.name + "]");
        }
    }

    void enterChaseState()
    {
        Debug.Log("[" + gameObject.name + "] has entered Chase state");
        if (movementController != null)
        {
            movementController.speed = movementSpeed;
            movementController.rotationTime = rotationTime;
            movementController.stopThreshold = stopThreshold;
        }
    }

    void chaseStateAction()
    {
        // Debug.Log("[" + gameObject.name + "] is currently in Chase state");
        if (player != null && movementController != null)
        {
            List<Node> pathNodes = Pathfinding.FindPath(gameObject.transform.position, player.transform.position);
            movementController.setPath(pathNodes);
            movementController.move();
        }
    }

    void exitChaseState()
    {
        Debug.Log("[" + gameObject.name + "] is exiting Chase state");
        lastSeenPlayerPosition = player.transform.position;
    }
}
