using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet.Example.ColliderRollbacks;
using UnityEngine;
using FishNet.Object;
using Unity.VisualScripting;

public class PlayerStateMachine : StateMachine
{
   [field: SerializeField] public InputReader InputReader { get; private set; }
   [field: SerializeField] public CharacterController CharacterController { get; private set; }
   [field: SerializeField] public Animator Animator { get; private set; }
   [field: SerializeField] public float Speed { get; private set; }
   [field: SerializeField] public float RotationSmoothValue { get; private set; }

   public Camera MainCamera{ get; private set; }
   public CinemachineBrain VirtualCamera{ get; private set; }

   public override void OnStartClient()
   {
      base.OnStartClient();
      if (base.IsOwner)
      {
         if (Camera.main != null) MainCamera = Camera.main;
         VirtualCamera = MainCamera.GetComponent<CinemachineBrain>();
         VirtualCamera.ActiveVirtualCamera.Follow = GameObject.FindGameObjectWithTag("CameraPos").transform;
         SwitchState(new PlayerFreeLookState(this));
      }
      else
      {
         gameObject.GetComponent<PlayerStateMachine>().enabled = false;
         VirtualCamera.enabled = false;
         MainCamera.enabled = false;
      }
   }
}
