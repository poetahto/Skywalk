using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class SubComponent<T> where T : Component
    {
        protected T Parent;

        protected SubComponent(T parent)
        {
            Parent = parent;
        }
        
        protected virtual void OnStart() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnDestroy() {}
    }
}