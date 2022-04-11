using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** A portion of this code originates from the video:
 * https://www.youtube.com/watch?v=8eWbSN2T8TE&t=25s **/


public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;
    public float stopThreshold = 0.05f;
    public float rotationTime = 1f;

    public Waypoints patrolPoints = null;
    private int listPos;
    private Quaternion qRotate; //quaternion rotate

    private EnemyRotation enemyRotation = null;

    public Vector2[] currentPath = null;
    public Vector2? currentDestination = null;

    public event Action arrivedAtDestinationEvent;

    // Use this for initialization
    void Start()
    {
        waitTime = startWaitTime;
        //listPos = Random.Range(0, patrolPoints.Length); //select a random waypoint
        listPos = 0;
        qRotate = transform.rotation;
        

        if (currentPath == null)
        {
            currentPath = new Vector2[] { };
        }

        enemyRotation = gameObject.GetComponent<EnemyRotation>();

    }

    // Update is called once per frame
    public void movePatrol()
    {
        if (currentPath.Length >= 1)
        {
            Vector2 destination = currentPath[listPos];

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (hasArrivedAtDestination(destination))
            {
                if (waitTime <= 0)
                {
                    //listPos = Random.Range(0, patrolPoints.Length);
                    listPos += 1;
                    if (listPos > (currentPath.Length - 1))
                    {
                        //parse through array when end of array is reached
                        listPos = 0;
                    }
                    
                    waitTime = startWaitTime;

                    Vector2 newDestination = currentPath[listPos];

                    if (enemyRotation != null)
                    {
                        enemyRotation.rotateTowards(newDestination, rotationTime);
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    public void move()
    {
        if (currentPath.Length >= 1)
        {
            Vector2 destination = currentPath[listPos];

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            enemyRotation.rotateTowards(destination, rotationTime);

            if (hasArrivedAtDestination(destination))
            {
                if (listPos < currentPath.Length - 1)
                {
                    listPos += 1;
                } else
                {
                    //Debug.Log("Destination: " + currentPath[listPos] + " Distance: " + Vector2.Distance(transform.position, destination));
                    //Debug.Break();
                    arrivedAtDestinationEvent?.Invoke();
                }
            }
        }
    }

    public void setPath(Transform[] pathPoints)
    {
        currentPath = new Vector2[pathPoints.Length];

        for (int i = 0; i < pathPoints.Length; i ++)
        {
            currentPath[i] = pathPoints[i].position;
        }

        listPos = 0;
    }

    public void setPath(List<Node> pathNodes)
    {
        currentPath = new Vector2[pathNodes.Count];

        for (int i = 0; i < pathNodes.Count; i ++)
        {
            currentPath[i] = pathNodes[i].position;
        }

        listPos = 0;
    }

    public void setDestination(Vector2 destination, Action callback = null)
    {
        List<Node> path = Pathfinding.FindPath(gameObject.transform.position, destination);

        if (path != null && path.Count > 0)
        {
            setPath(path);
        } else
        {
            if (callback != null)
            {
                callback();
            }

            arrivedAtDestinationEvent?.Invoke();
        }
    }

    bool hasArrivedAtDestination(Vector2 destination)
    {
        return Vector2.Distance(transform.position, destination) < stopThreshold;
    }

    public void setPathToPatrol()
    {
        setPath(patrolPoints.waypoints);
    }

}
