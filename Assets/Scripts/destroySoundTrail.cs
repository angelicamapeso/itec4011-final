using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySoundTrail : MonoBehaviour
{
    public float bitLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bitLifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i ++)
        {
            StateMachine sm = enemies[i].GetComponent<StateMachine>();
            if (sm != null)
            {
                sm.lastFootPrints.Remove(gameObject.GetInstanceID());
            }
        }
    }
}
