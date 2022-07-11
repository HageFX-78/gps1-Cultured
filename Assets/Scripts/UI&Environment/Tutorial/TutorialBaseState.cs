using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBaseState
{
    public abstract void EnterState(TutorialStateManager tutorial);
    public abstract void UpdateState(TutorialStateManager tutorial);
    public abstract void OnTriggerEnter2D(TutorialStateManager tutorial, Collider2D trigger);
    public abstract void OnCollisionEnter2D(TutorialStateManager tutorial, Collision2D col);
}
