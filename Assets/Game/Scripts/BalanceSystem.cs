﻿using poetools.Runtime.Systems;
using UnityEngine;

namespace Game.Scripts
{
    public class BalanceSystem : MonoBehaviour
    {
        [SerializeField] private Transform yawTransform;
        [SerializeField] private RotationSystem rotationSystem;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float stabilizeSpeed = 1f;
        [SerializeField] private float fallThreshold = 45f;
        [SerializeField] private float fallSpeed = 1f;
        
        public void Destabilize(float amount)
        {
            rotationSystem.Roll += amount;
            
            if (rotationSystem.Roll > fallThreshold)
                rigidbody.AddForce((-yawTransform.right).normalized * fallSpeed, ForceMode.VelocityChange);
            
            if (rotationSystem.Roll < -fallThreshold)
                rigidbody.AddForce((yawTransform.right).normalized * fallSpeed, ForceMode.VelocityChange);
        }

        public void Stabilize()
        {
            rotationSystem.Roll = Mathf.Lerp(rotationSystem.Roll, 0, stabilizeSpeed * Time.deltaTime);
        }
    }
}