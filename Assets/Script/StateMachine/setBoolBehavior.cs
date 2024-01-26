﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.StateMachine
{
    public class setBoolBehavior :StateMachineBehaviour
    {
        public string boolName;
        public bool valueOnEnter, valueOnExit;
        public bool updateOnStateMachine;
        public bool updateOnState;
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState) { animator.SetBool(boolName, valueOnEnter); }
        }
        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState) { animator.SetBool(boolName, valueOnExit); }
        }
        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
                animator.SetBool(boolName, valueOnEnter);
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
                animator.SetBool(boolName, valueOnExit);
        }
    }
}
