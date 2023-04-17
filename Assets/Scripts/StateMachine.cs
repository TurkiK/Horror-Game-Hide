using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public ChaseState chaseState;

    public void Initialise()
    {
        patrolState = new PatrolState();
        chaseState = new ChaseState();
        ChangeState(patrolState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Enter();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //Check activeState != null
        if(activeState != null)
        {
            //run cleanup on activestate.
            activeState.Exit();
        }
        //change into new state
        activeState = newState;

        //fail-safe null check to make sure new state wasn't null.
        if(activeState != null)
        {
            //Setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
