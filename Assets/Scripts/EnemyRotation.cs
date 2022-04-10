using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    // STEERING VARIABLES
    // max rotation speed
    public float maxRotation;
    // radius for arriving
    public float targetRadius;
    // radius for slowing
    public float slowRadius;
    // time to target
    public float timeToTarget = 0.1f;

    // VARIABLES TO UPDATE ORIENTATION
    // change in orientation (deg/sec)
    public float rotation = 0;
    // change in rotation (deg/sec/sec)
    public float angularAcceleration = 0;

    // PERFORM CALCULATIONS IF TARGET ORIENTATION SET:
    public float? targetOrientation = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        calculateAngularAcceleration();

        // Update orientation
        transform.rotation = Quaternion.Euler(0, 0, getCurrentOrientation() + rotation * Time.deltaTime);
        // Update rotation
        rotation += angularAcceleration * Time.deltaTime;
    }

    float getCurrentOrientation()
    {
        return transform.rotation.eulerAngles.z;
    }

    void setOrientation(float orientation)
    {
        transform.rotation = Quaternion.Euler(0, 0, orientation);
    }

    public void rotateTowards(Vector2 point)
    {
        float totalRotation = Mathf.Atan2(
            point.y - transform.position.y,
            point.x - transform.position.x) * Mathf.Rad2Deg;

        totalRotation -= 90;

        targetOrientation = totalRotation;
    }

    void rotateTowards(float angle)
    {
        targetOrientation = angle;
    }

    void calculateAngularAcceleration()
    {
        if (targetOrientation != null)
        {
            float rotation = Mathf.DeltaAngle(getCurrentOrientation(), (float) targetOrientation);
            float rotationSize = Mathf.Abs(rotation);

            if (rotationSize < targetRadius)
            {
                this.rotation = 0;
                angularAcceleration = 0;
                setOrientation((float) targetOrientation);
                targetOrientation = null;
                return;
            }

            float targetRotation = maxRotation;
            
            if (rotationSize < slowRadius)
            {
                targetRotation = maxRotation * rotationSize / slowRadius;
            }
            
            targetRotation *= rotation / rotationSize;

            angularAcceleration = (targetRotation - this.rotation) / timeToTarget;
        }
    }
}
