using FMODUnity;
using UnityEngine;

[CreateAssetMenu]
public class SurfaceSettings : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float friction = 4.5f;
        
    [Header("Audio Settings")]
    [SerializeField] private EventReference stepSound;
    [SerializeField] private EventReference jumpSound;
    [SerializeField] private EventReference landSound;
        
    // ---- Properties ----

    public float Friction => friction;
    public EventReference StepSound => stepSound;
    public EventReference JumpSound => jumpSound;
    public EventReference LandSound => landSound;
}