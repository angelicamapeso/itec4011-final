using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



// This code is taken from the Unity forums
//https://forum.unity.com/threads/gui-label-blinking-text.17337/


public class blinkingGUI : MonoBehaviour
{

    Text flashingText;

    void Start()
    {
        //get the Text component
        flashingText = GetComponent<Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            //set the Text's text to blank
            flashingText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.5f);
            //display text for the next 0.5 seconds
            flashingText.text = "START";
            yield return new WaitForSeconds(.5f);
        }
    }
}
