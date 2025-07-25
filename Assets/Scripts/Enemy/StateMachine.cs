using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;


    public void Initialise() 
    {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null) 
        {
            activeState.Perform();
        }
    }
    public void ChangeState(BaseState newState)
    {
        //check activestate != null
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;

        //fail-safe null checck to make sure new state wasnt null
        if (activeState != null) 
        {
            //setup new data
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        
        }
    }
}
