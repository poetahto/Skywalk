using System;
using poetools.Runtime.Systems;
using UnityEngine;

/// <summary>
/// Controls the jumping system with input.
/// </summary>

public class InputJumpingController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private JumpingSystem system;
    [SerializeField] private Controls controls;
    
    private Buffer _jumpBuffer = new Buffer();

    private void OnEnable()
    {
        system.onJump.AddListener(ClearBuffer);
    }

    private void OnDisable()
    {
        system.onJump.RemoveListener(ClearBuffer);
        system.UpdateGravity(false);
        _jumpBuffer.Clear();
    }

    private void ClearBuffer()
    {
        _jumpBuffer.Clear();
    }
    
    private void Update()
    {
        bool wantsToJump = Input.GetKeyDown(controls.jump);

        if (system.JumpSettings.HoldAndJump)
            wantsToJump |= Input.GetKey(controls.jump);
        
        if (system.JumpSettings.ScrollToJump)
            wantsToJump |= Input.mouseScrollDelta != Vector2.zero;
        
        // Instead of jumping whenever we press a button, we queue a jump instead.
        // This will still be responsive when grounded, and won't eat your inputs when falling.
        
        if (wantsToJump)
            _jumpBuffer.Queue();

        if (wantsToJump || _jumpBuffer.IsQueued(system.JumpSettings.JumpBufferTime))
            system.TryToJump();
    }

    private void FixedUpdate()
    {
        system.UpdateGravity(Input.GetKey(controls.jump));
    }
}