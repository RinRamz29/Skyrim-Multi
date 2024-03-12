using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        StateMachine.CharacterController.Move(movement * (StateMachine.Speed * deltaTime));
        FaceMovementDirection(movement, deltaTime);
        
        if (StateMachine.InputReader.MovementValue == Vector2.zero)
        {
            StateMachine.Animator.SetFloat(_freeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        
        StateMachine.Animator.SetFloat(_freeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = StateMachine.MainCamera.transform.forward;
        Vector3 right = StateMachine.MainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();

        return forward * StateMachine.InputReader.MovementValue.y + right * StateMachine.InputReader.MovementValue.x;

    }
    
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        StateMachine.transform.rotation = Quaternion.Lerp(StateMachine.transform.rotation, 
            Quaternion.LookRotation(StateMachine.MainCamera.transform.forward), deltaTime * StateMachine.RotationSmoothValue);
    }
}
