using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;

    public GameObject player = null;
    public Vector2? lastInterestPoint = null;
    public EnemyMovement enemyMovement = null;
    public EnemyRotation enemyRotation = null;
    public EnemySight enemySight = null;

    public List<int> lastFootPrints = null;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No Player in scene!");
        }

        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.LogError("No 'EnemyMovement' script on [" + gameObject.name + "]");
        }

        enemyRotation = gameObject.GetComponent<EnemyRotation>();
        if (enemyMovement == null)
        {
            Debug.LogError("No 'EnemyRotation' script on [" + gameObject.name + "]");
        }

        enemySight = gameObject.GetComponent<EnemySight>();
        if (enemyMovement == null)
        {
            Debug.LogError("No 'EnemySight' script on [" + gameObject.name + "]");
        }

        lastFootPrints = new List<int>();
    }

    void Start()
    {
        currentState = initialState;
        currentState.executeEntryActions?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Transition triggered = null;

        if (currentState != null)
        {
            foreach (Transition transition in currentState.transitions)
            {
                if (transition.isTriggered())
                {
                    triggered = transition;
                    break;
                }
            }

            if (triggered != null)
            {
                State targetState = triggered.targetState;

                currentState.executeExitActions?.Invoke();
                triggered.executeTransitionActions?.Invoke();
                targetState.executeEntryActions?.Invoke();

                currentState = targetState;
            }
            else
            {
                currentState.executeStateActions?.Invoke();
            }
        }
    }

    public void SetLastPlayerInterestPoint()
    {
        if (player != null && enemyMovement != null)
        {
            Main playerScript = player.GetComponent<Main>();
            if (playerScript != null)
            {
                Vector2 playerPosition = player.transform.position;
                Vector2 playerDirection = playerScript.GetPlayerDirection();

                float d = Vector2.Distance(playerPosition, transform.position);
                // Scaled down to avoid from going outside of grid
                float timeToGetToPlayer = (d / enemyMovement.speed) * 0.5f;

                lastInterestPoint = playerPosition + (playerDirection * timeToGetToPlayer * playerScript.currentSpeed);
            }
        }
    }
}
