using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action OnJumpEvent;
    public event Action OnBlockEvent;

    public Vector2 MovementValue { get; private set; }
    private Controls _controls;
    
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        OnJumpEvent?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        OnBlockEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }
}
