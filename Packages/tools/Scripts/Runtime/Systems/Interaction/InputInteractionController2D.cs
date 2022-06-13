using UnityEngine;

namespace Systems.Interaction
{
    public class InputInteractionController2D : MonoBehaviour
    {
        [SerializeField] private InteractionSystem system;
        [SerializeField] private Controls controls;
        [SerializeField] private Camera viewCamera;
        
        private void Update()
        {
            system.HoldingInteract = Input.GetKey(controls.interact);
            system.ViewRay = viewCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}