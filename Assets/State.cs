using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public delegate void Actions();
    public delegate void EntryActions();
    public delegate void ExitActions();

    public Actions executeStateActions;
    public EntryActions executeEntryActions;
    public ExitActions executeExitActions;

    public Transition[] transitions;
}
