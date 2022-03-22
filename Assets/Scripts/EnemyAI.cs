using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** A portion of this code originates from the video:
 * https://www.youtube.com/watch?v=8eWbSN2T8TE&t=25s **/


public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int listPos;
    private Quaternion qRotate; //quaternion rotate

    // Use this for initialization
    void Start()
    {
        waitTime = startWaitTime;
        //listPos = Random.Range(0, moveSpots.Length); //select a random waypoint
        listPos = 0;
        qRotate = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[listPos].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[listPos].position) < 0.2f)
        {

            if (waitTime <= 0)
            {
                //listPos = Random.Range(0, moveSpots.Length);
                listPos += 1;
                if (listPos > (moveSpots.Length - 1))
                {
                    //parse through array when end of array is reached
                    listPos = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision) //scene change here
    //{
    //    if (collision.gameObject.name.Equals("Player"))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}
}
