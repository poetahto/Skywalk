using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
// Events that provide information about a collision; these handled well by scripts
public struct CollisionEvents 
{
    public UnityEvent<Collider> collisionEnter;
    public UnityEvent<Collider> collisionExit;
}
    
// Wraps a trigger Collider and provides UnityEvents for OnTriggerEnter / Exit / Stay
[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    [SerializeField] bool triggerOnce;
    public CollisionEvents events;

    bool _isTriggered;
    
    private void Awake()
    {
        _isTriggered = false;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerOnce || !_isTriggered)
        {
            events.collisionEnter?.Invoke(other);
            _isTriggered = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        events.collisionExit.Invoke(other);
    }
}