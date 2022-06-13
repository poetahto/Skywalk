using UnityEngine;

namespace poetools.Runtime.Systems
{
    public abstract class MovementStrategy : ScriptableObject
    {
        public struct Context
        {
            public Vector3 TargetDirection;
            public Vector3 CurrentVelocity;
            public float CurrentSpeed;
            public float SpeedMultiplier;
            public CollisionData Data;
        }

        public abstract Vector3 UpdateVelocity(Context context);
    }
}