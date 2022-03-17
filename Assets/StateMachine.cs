using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = initialState;
    }

    // Update is called once per frame
    void Update()
    {
        Transition triggered = null;

        foreach (Transition transition in currentState.transitions)
        {
            if (transition.isTriggered())
            {
                triggered = transition;
                break;
            }
        }

        if (triggered != null)
        {
            State targetState = triggered.targetState;

            currentState.executeStateActions?.Invoke();
            triggered.executeTransitionActions?.Invoke();
            targetState.executeEntryActions?.Invoke();

            currentState = targetState;
        } else
        {
            currentState.executeStateActions?.Invoke();
        }
    }


}
