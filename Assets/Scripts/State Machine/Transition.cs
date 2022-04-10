using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public State targetState;
    public delegate void TransitionActions();

    public TransitionActions executeTransitionActions;

    public abstract bool isTriggered();

    protected StateMachine sm = null;

    protected void Start()
    {
        sm = gameObject.GetComponent<StateMachine>();
        if (sm == null)
        {
            Debug.LogError("No 'StateMachine' script on [" + gameObject.name + "]");
        }
    }
}
