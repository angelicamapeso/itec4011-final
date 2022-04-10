using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = initialState;
        currentState.executeEntryActions?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Transition triggered = null;

        if (currentState != null)
        {
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

                currentState.executeExitActions?.Invoke();
                triggered.executeTransitionActions?.Invoke();
                targetState.executeEntryActions?.Invoke();

                currentState = targetState;
            }
            else
            {
                currentState.executeStateActions?.Invoke();
            }
        }
    }

}
