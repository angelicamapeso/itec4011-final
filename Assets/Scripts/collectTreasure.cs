using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectTreasure : MonoBehaviour
{
    public bool hasTreasure = false;
    public float slowedSpeed;
    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Treasure"))
        {
            hasTreasure = true;
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            Main mainScript = playerObject.GetComponent<Main>();
            mainScript.maxSpeed = mainScript.maxSpeed * slowedSpeed;
        }
        
        if (collision.gameObject.name.Equals("Homebase Area"))
        {
            if (hasTreasure)
            {
                SceneManager.LoadScene(1); //won game
            }
        }
    }
}
