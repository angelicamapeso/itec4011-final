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
}
