using System.Collections;
using System.Collections.Generic;
using FishNet.Demo.AdditiveScenes;
using UnityEngine;

public abstract class PlayerBaseState : State
{ 
    protected PlayerStateMachine StateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }
}
