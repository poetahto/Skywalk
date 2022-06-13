﻿// using JetBrains.Annotations;
// using poetools.Runtime.Systems;
// using UnityEngine;
//
// public class CrouchSystem : MonoBehaviour
// {
//     [Header("Settings")] 
//     [SerializeField] private bool showDebug;
//     [SerializeField, Range(0, 5)] private float standingHeight = 2f;
//     [SerializeField] private Vector3 standingCenter = new Vector3(0, -0.5f, 0);
//     [SerializeField, Range(0, 5)] private float crouchHeight = 1f;
//     [SerializeField] private Vector3 crouchCenter = Vector3.zero;
//     [SerializeField, Range(0, 1)] private float crouchSpeedMultiplier = 0.25f;
//     [SerializeField] private float radiusBuffer = 0.1f;
//     [SerializeField] private float heightBuffer = 0.1f;
//     
//     [Header("Dependencies")]
//     [SerializeField] private MovementSystem movementSystem;
//     [SerializeField] private BoxCollider playerCollider;
//
//     private bool _blockedAbove;
//     
//     [PublicAPI] public bool IsCrouching { get; set; }
//
//     private void Update()
//     {
//         _blockedAbove = CheckIfBlockedAbove();
//         
//         UpdateCollider();
//         UpdateSpeed();            
//     }
//
//     private bool CheckIfBlockedAbove()
//     {
//         var ray = new Ray(playerCollider.bounds.center, playerCollider.transform.up);
//         float radius = playerCollider.radius - radiusBuffer;
//         float maxDistance = playerCollider.height / 2 + heightBuffer;
//         
//         return Physics.SphereCast(ray, radius, maxDistance);
//     }
//
//     private void UpdateCollider()
//     {
//         if (IsCrouching)
//         {
//             playerCollider.height = crouchHeight;
//             playerCollider.center = crouchCenter;
//         }
//         
//         else if ((IsCrouching || _blockedAbove) == false)
//         {
//             playerCollider.height = standingHeight;
//             playerCollider.center = standingCenter;
//         }
//     }
//
//     private void UpdateSpeed()
//     {
//         if (IsCrouching)
//             movementSystem.SpeedMultiplier = crouchSpeedMultiplier;
//
//         else if ((IsCrouching || _blockedAbove) == false)
//             movementSystem.SpeedMultiplier = 1;
//     }
//
//     private void OnGUI()
//     {
//         if (showDebug)
//             DrawDebugUI();
//     }
//
//     private void DrawDebugUI()
//     {
//         GUILayout.Label($"Holding crouch: {IsCrouching}");
//         GUILayout.Label($"Blocked above: {_blockedAbove}");
//     }
// }