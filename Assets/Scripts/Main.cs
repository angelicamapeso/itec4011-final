using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float speed;
    private float minSpeed;
    public float maxSpeed;
    Animator animator;

    void Start()
    {
        //pull info from the characters Animator onto animator
        //animator = GetComponent<Animator>();
        //Application.targetFrameRate = 200;
    }

    // Update is called once per frame
    void Update()
    {
        //call movement function
        Movement();

    }

    void Movement()
    {
        //animator.SetFloat("HMove_Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        //animator.SetFloat("MoveSpeed", Mathf.Abs(Input.GetAxis("Vertical")));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
            //animator.SetInteger("direction", 2);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
            //animator.SetInteger("direction", 2);

        }
        if ((Input.GetAxisRaw("Vertical") > 0))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
            //animator.SetInteger("direction", 0);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
            //animator.SetInteger("direction", 1);
        }

    }
}
