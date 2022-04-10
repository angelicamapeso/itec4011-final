using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrailBit : MonoBehaviour
{
    public GameObject soundTrailBitPrefab;
    public GameObject playerObject = null;
    private GameObject soundBitObject = null;
    public float respawnTime;

    public float bitLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(trailSpawnIntervals());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnTrailBit(){
        GameObject a = Instantiate(soundTrailBitPrefab) as GameObject; //add sound trail bit to scene
        a.transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y); //spawn on player location
    }
    IEnumerator trailSpawnIntervals()
    {
        while (true) //while game is running
        {
            yield return new WaitForSeconds(respawnTime);
            spawnTrailBit();
        }
    }
}
