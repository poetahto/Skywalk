using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class ElevatorController : MonoBehaviour
    {
        [SerializeField] private bool showDebug;
        
        [Header("Doors")]
        [SerializeField] private float doorOpenTime = 0.5f;
        [SerializeField] private Transform leftDoor;
        [SerializeField] private Transform rightDoor;
        [SerializeField] private Collider lockCollider;
        [SerializeField] private float OpenAmount = 0.9f;
        [SerializeField] private UnityEvent onDoorsOpen;
        [SerializeField] private UnityEvent onDoorsClose;

        [Header("Delay")]
        [SerializeField] private bool openDelay;
        [SerializeField] private float delayTime;

        private Vector3 _leftOpen;
        private Vector3 _leftClosed;
        
        private Vector3 _rightOpen;
        private Vector3 _rightClosed;

        private void Awake()
        {
            lockCollider.enabled = false;

            _leftClosed = leftDoor.position;
            _leftOpen = _leftClosed + leftDoor.forward * OpenAmount;
            
            _rightClosed = rightDoor.position;
            _rightOpen = _rightClosed + rightDoor.forward * OpenAmount;
        }

        private IEnumerator Start()
        {
            if (openDelay) { 
                yield return new WaitForSeconds(delayTime);
                Open();
            }


        }

        private IEnumerator DoorAnimateCoroutine(
            Vector3 rightStart, Vector3 rightEnd, 
            Vector3 leftStart, Vector3 leftEnd
            )
        {
            float elapsedTime = 0;

            while (elapsedTime < doorOpenTime)
            {
                float t = EaseInOutSine(elapsedTime / doorOpenTime);
                
                rightDoor.position = Vector3.Lerp(rightStart, rightEnd, t);
                leftDoor.position = Vector3.Lerp(leftStart, leftEnd, t);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            lockCollider.enabled = false;
        }

        [PublicAPI]
        public void Open()
        {
            onDoorsOpen.Invoke();
            StopAllCoroutines();
            
            StartCoroutine(DoorAnimateCoroutine(
                _rightClosed, _rightOpen, 
                _leftClosed, _leftOpen
                ));
        }

        [PublicAPI]
        public void Close()
        {
            onDoorsClose.Invoke();
            StopAllCoroutines();
            
            StartCoroutine(DoorAnimateCoroutine(
                _rightOpen, _rightClosed, 
                _leftOpen, _leftClosed
            ));

            lockCollider.enabled = true;
        }

        private static float EaseInOutSine(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }

        private void OnGUI()
        {
            if (!showDebug)
                return;
            
            if (GUILayout.Button("Open"))
                Open();
            
            if (GUILayout.Button("Close"))
                Close();
        }
    }
}