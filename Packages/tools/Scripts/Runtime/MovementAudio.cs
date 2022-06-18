using FMODUnity;
using JetBrains.Annotations;
using poetools;
using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private CollisionData collisionData;
        
    [Header("Settings")]
    [SerializeField] private float stepDistance = 2f;
    [SerializeField] private SurfaceSettings defaultSurfaceSettings;
        
    private Surface _surface;
    private float _displacement;
    private Vector3 _previousPosition;

    private void Awake()
    {
        collisionData.onEnterCollision.AddListener((col1, col2) => _displacement = 0);
        // _displacement = stepDistance;
        _previousPosition = transform.position; 
    }

    private void Update()
    {
        if (collisionData.IsGrounded)
            CheckForStep();
            
        _previousPosition = transform.position;
    }

    private void CheckForStep()
    {
        _displacement += Vector3.Distance(transform.position, _previousPosition);
            
        if (_displacement > stepDistance)
            PlayStepSound();
    }

    [PublicAPI]
    public void PlayStepSound()
    {
        _displacement = 0;
            
        SurfaceSettings settings = GetCurrentSettings();
        RuntimeManager.PlayOneShot(settings.StepSound);
        // audioSource.PlayOneShot(settings.StepSound, 0.5f);
    }

    [PublicAPI]
    public void PlayJumpSound()
    {
        SurfaceSettings settings = GetCurrentSettings();
        RuntimeManager.PlayOneShot(settings.JumpSound);
        // audioSource.PlayOneShot(settings.JumpSound, 1f);
    }

    [PublicAPI]
    public void PlayLandSound()
    {
        SurfaceSettings settings = GetCurrentSettings();
        RuntimeManager.PlayOneShot(settings.LandSound);
        // audioSource.PlayOneShot(settings.LandSound, 0.25f);
    }

    private SurfaceSettings GetCurrentSettings()
    {
        return collisionData.ConnectedCollider != null && collisionData.ConnectedCollider.TryGetComponent(out _surface)
            ? _surface.Settings
            : defaultSurfaceSettings;
    }
}