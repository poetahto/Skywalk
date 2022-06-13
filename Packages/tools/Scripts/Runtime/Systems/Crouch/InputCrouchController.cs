// using UnityEngine;
//
// namespace Systems.Crouch
// {
//     public class InputCrouchController : MonoBehaviour
//     {
//         [SerializeField] private Controls controls;
//         [SerializeField] private CrouchSystem system;
//         
//         private void Update()
//         {
//             if (Input.GetKeyDown(controls.sneak))
//                 system.IsCrouching = true;
//             
//             else if (Input.GetKeyUp(controls.sneak))
//                 system.IsCrouching = false;
//         }
//     }
// }