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
    public void delayedMove()
    {
        if (currentPath.Length >= 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPath[listPos], speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentPath[listPos]) < stopThreshold)
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

                    if (enemyRotation != null)
                    {
                        enemyRotation.rotateTowards(currentPath[listPos], rotationTime);
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
            transform.position = Vector2.MoveTowards(transform.position, currentPath[listPos], speed * Time.deltaTime);
            enemyRotation.rotateTowards(currentPath[listPos], rotationTime);

            if (Vector2.Distance(transform.position, currentPath[listPos]) < stopThreshold)
            {
                listPos += 1;
                if (listPos >= currentPath.Length)
                {
                    listPos = 0;
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

    public void setPathToPatrol()
    {
        setPath(patrolPoints.waypoints);
    }

    //private void OnTriggerEnter2D(Collider2D collision) //scene change here
    //{
    //    if (collision.gameObject.name.Equals("Player"))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}
}
