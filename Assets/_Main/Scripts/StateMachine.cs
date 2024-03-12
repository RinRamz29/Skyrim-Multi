using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public abstract class StateMachine : NetworkBehaviour
{
   private State _currentState;

   private void Update()
   {
      _currentState?.Tick(Time.deltaTime);
   }

   public void SwitchState(State newState)
   {
      _currentState?.Exit();
      _currentState = newState;
      _currentState?.Enter();
   }
}
