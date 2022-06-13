using UnityEngine;
using UnityEngine.Events;

public class CheckpointGroup : MonoBehaviour
{
    [SerializeField] private UnityEvent<Transform> activatedCheckpoint;
    [SerializeField] private Transform defaultCheckpoint;
    [SerializeField] private bool showDebug;
    
    private Transform _currentCheckpoint;
    public Transform CurrentCheckpoint
    {
        get => _currentCheckpoint;
        set
        {
            if (_currentCheckpoint != value)
                activatedCheckpoint.Invoke(value);
            
            _currentCheckpoint = value;
        }
    }

    private void Start()
    {
        CurrentCheckpoint = defaultCheckpoint;
    }

    private void OnGUI()
    {
        if (showDebug)
        {
            string checkpointName = CurrentCheckpoint == null ? "None" : CurrentCheckpoint.name;
            GUILayout.Label($"Current Checkpoint: {checkpointName}");
        }
    }
}