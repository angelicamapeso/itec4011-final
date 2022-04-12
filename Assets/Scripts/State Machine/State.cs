using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code adapted from Algorithms found in:
* Millington, I. (2019). AI for Games. (3 ed.). CRC Press.
*/
public abstract class State : MonoBehaviour
{
    public delegate void Actions();
    public delegate void EntryActions();
    public delegate void ExitActions();

    public Actions executeStateActions;
    public EntryActions executeEntryActions;
    public ExitActions executeExitActions;

    public Transition[] transitions;

    protected StateMachine sm = null;

    protected void OnEnable()
    {
        sm = gameObject.GetComponent<StateMachine>();
        if (sm == null)
        {
            Debug.LogError("No 'StateMachine' script on [" + gameObject.name + "]");
        }
    }
}
