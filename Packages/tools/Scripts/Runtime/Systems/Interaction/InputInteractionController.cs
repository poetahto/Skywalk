using UnityEngine;

/// <summary>
/// Controls the interaction system with input. 
/// </summary>

public class InputInteractionController : MonoBehaviour
{
    [SerializeField] private InteractionSystem system;
    [SerializeField] private Controls controls;
    
    [Tooltip("Scroll wheel speed")]
    [SerializeField] private float scrollWheelSpeed = 0.5f;

    [Tooltip("Distance between player and GameObject")]
    [SerializeField] private float interactableDistance = 0.75f;

    private Transform _camaraTransform;

    private void Awake()
    {
        _camaraTransform = Camera.main != null
            ? Camera.main.transform
            : transform;
    }

    private void Update()
    {
        system.HoldingInteract = Input.GetKey(controls.interact);
        system.ViewRay = new Ray(system.lookDirection.position, system.lookDirection.forward);

        if (system.HoldingInteract && system.HasInteractable)
            ScrollWheelController();
    }

    private void OnDisable()
    {
        system.HoldingInteract = false;
    }

    private void ScrollWheelController()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        if (system.CurrentInteractable.TryGetComponent(out MovableObject targetObject) && !targetObject.HasCollision)
        {
            Vector3 targetPosition = targetObject.TargetPosition;
            Vector3 cameraPosition = _camaraTransform.position;
            Vector3 directionToTarget = (targetPosition - cameraPosition ).normalized;
            Vector3 updatedTargetPosition = targetPosition + directionToTarget * scrollInput * scrollWheelSpeed;
            
            float distanceFromObject = Vector3.Distance(updatedTargetPosition, cameraPosition);
            bool safelyScrollOut = scrollInput > 0 && distanceFromObject < system.CurrentInteractable.InteractRange;
            bool safelyScrollIn = scrollInput < 0 && distanceFromObject > interactableDistance;

            // Essentially constrain how close and how far the object can be held from the player.
            // if (safelyScrollOut || safelyScrollIn)
            //     targetObject.TargetPosition = updatedTargetPosition;
        }
    }
}