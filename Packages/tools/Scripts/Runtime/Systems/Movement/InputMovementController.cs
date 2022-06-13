using System;
using poetools.Runtime.Systems;
using UnityEngine;

/// <summary>
/// Controls the movement system with input.
/// </summary>

public class InputMovementController : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField] private MovementSystem system;
    [SerializeField] private Controls controls;
    [SerializeField] private Transform playerYaw;
    [SerializeField] private bool upAxisEnabled;

    private void Update()
    {
        float forwardAxis = CalculateAxis(controls.forward, controls.backward);
        float rightAxis = CalculateAxis(controls.right, controls.left);
        float upAxis = CalculateAxis(controls.jump, controls.sneak);
        
        Vector3 forward = playerYaw.forward * forwardAxis;
        Vector3 right = playerYaw.right * rightAxis;
        Vector3 up = upAxisEnabled ? playerYaw.up * upAxis : Vector3.zero;

        system.TargetDirection = (forward + right + up).normalized;
    }

    private void OnDisable()
    {
        system.TargetDirection = Vector3.zero;
    }

    private static float CalculateAxis(KeyCode positive, KeyCode negative)
    {
        float result = 0;
        
        if (Input.GetKey(positive))
            result += 1;
            
        if (Input.GetKey(negative))
            result -= 1;

        return result;
    }
}