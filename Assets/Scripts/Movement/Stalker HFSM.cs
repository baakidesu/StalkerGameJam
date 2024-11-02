using System;
using UnityEngine;
using UnityHFSM;
using System.Collections;
using System.Collections.Generic;

public class StalkerHFSM : MonoBehaviour
{

    private StateMachine hfsm;

    private string _triggerHide = "moveToHide";
    private string _triggerLose = "lostGame";
    private string _triggerWalk = "Walk";
     void Start()
    {
        hfsm = new StateMachine();
        //todo
        hfsm.AddState("Hide", new HideState(this,false));
        hfsm.AddState("Lose", new LoseState(this,false));
        hfsm.AddState("Walk", new WalkState(this,false));
        
        hfsm.AddTriggerTransition(_triggerHide, "Walk", "Hide");
        hfsm.AddTriggerTransition(_triggerWalk, "Hide", "Walk");
        hfsm.AddTriggerTransitionFromAny(_triggerLose, "Lose");
        
        hfsm.SetStartState("Walk");
        
        hfsm.Init();
        
    }
     
    void Update()
    {
        hfsm.OnLogic();
    }

    public void TriggerLose()
    {
        hfsm.Trigger(_triggerLose);
    }
    public void TriggerWalk()
    {
        hfsm.Trigger(_triggerWalk);
    }
    public void TriggerHide()
    {
        hfsm.Trigger(_triggerHide);
    }
}
