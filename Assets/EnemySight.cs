using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float distanceToSee = 4.5f;
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No player is present in the scene!");
        }
    }

    public bool canSeePlayer()
    {
        return (player != null && canSeeObject(player));
    }

    public bool canSeeObject(GameObject obj)
    {
        return (isObjectInFront(obj) && !isObjectBlocked(obj));
    }

    bool isObjectInFront(GameObject obj)
    {
        Vector2 forward = transform.TransformDirection(Vector2.up);
        Vector2 toObject = obj.transform.position - transform.position;

        if (Vector2.Dot(forward, toObject) > 0 && Vector2.Angle(forward, toObject) < 45)
        {
            return true;
        }

        return false;
    }

    bool isObjectBlocked(GameObject obj)
    {
        Vector2 toObject = (obj.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, toObject, distanceToSee, LayerMask.GetMask("Collidable", LayerMask.LayerToName(obj.layer)));

        if (hit.collider != null)
        {
            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y);

            if (!hit.collider.CompareTag(obj.tag))
            {
                Debug.DrawRay(transform.position, hitPoint - transform.position, Color.blue);
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, hitPoint - transform.position, Color.red);
                return false;
            }

        }

        return true;
    }
}
