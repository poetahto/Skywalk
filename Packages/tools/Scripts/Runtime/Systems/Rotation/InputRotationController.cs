using poetools.Runtime.Systems;
using UnityEngine;

/// <summary>
/// Controls the rotation system with input.
/// </summary>

public class InputRotationController : MonoBehaviour
{
    [SerializeField] private Controls controls;
    [SerializeField] private RotationSystem system;
    [SerializeField] private bool hideCursor;

    private void OnEnable()
    {
        if (hideCursor)
            SetCursor(false);
    }

    private void OnDisable()
    {
        if (hideCursor)
            SetCursor(true);
    }
    
    private static void SetCursor(bool isEnabled)
    {
        Cursor.visible = isEnabled;
        Cursor.lockState = isEnabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        float yMultiplier = controls.invertY ? 1 : -1;

        system.Pitch += Input.GetAxisRaw("Mouse Y") * controls.sensitivity * Time.timeScale * yMultiplier;
        system.Yaw += Input.GetAxisRaw("Mouse X") * controls.sensitivity * Time.timeScale;
    }
}