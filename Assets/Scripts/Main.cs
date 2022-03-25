using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float speed;
    private float minSpeed;
    public float maxSpeed;

    public LineRenderer lineRenderer = null;
    public float soundRadius = 0;
    public float maxSoundRadius = 1.5f;
    // increment value per second
    public float incrementSoundBy = 0.5f;

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
        Sound();
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

    void Sound()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            ResetSoundRadius();
        }
        else
        {
            IncreaseSoundRadius();
        }

        DrawSoundRadius();
    }

    void DrawSoundRadius()
    {
        if (lineRenderer != null)
        {
            int steps = 25;
            lineRenderer.positionCount = steps;

            for (int currentStep = 0; currentStep < steps; currentStep ++)
            {
                float circumferenceProgress = (float)currentStep / (steps - 1);

                float currentRadian = circumferenceProgress * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(currentRadian);
                float yScaled = Mathf.Sin(currentRadian);

                float x = xScaled * soundRadius + transform.position.x;
                float y = yScaled * soundRadius + transform.position.y;

                Vector3 currentPosition = new Vector3(x, y, 0);

                lineRenderer.SetPosition(currentStep, currentPosition);
            }
        }
    }

    void IncreaseSoundRadius()
    {
        if (soundRadius < maxSoundRadius)
        {
            soundRadius += Time.deltaTime * incrementSoundBy;
            if (soundRadius >= maxSoundRadius)
            {
                soundRadius = maxSoundRadius;
            }
        }
    }

    void ResetSoundRadius()
    {
        soundRadius = 0;
    }
}
