using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Game.Scripts
{
    [RequireComponent(typeof(DialogueRunner))]
    public class FPSDialogueHelper : MonoBehaviour
    {
        private List<Behaviour> _inputBehaviours = new List<Behaviour>();
        
        private void Start()
        {
            _inputBehaviours.Add(FindObjectOfType<InputMovementController>());
            _inputBehaviours.Add(FindObjectOfType<InputRotationController>());
            _inputBehaviours.Add(FindObjectOfType<InputJumpingController>());
            _inputBehaviours.Add(FindObjectOfType<InputInteractionController>());
            
            var dialogueRunner = GetComponent<DialogueRunner>();
            dialogueRunner.onNodeStart.AddListener(HandleNodeStart);
            dialogueRunner.onNodeComplete.AddListener(HandleNodeComplete);
        }

        private void HandleNodeComplete(string arg0)
        {
            SetPlayerInput(true);
        }

        private void HandleNodeStart(string arg0)
        {
            SetPlayerInput(false);
        }

        private void SetPlayerInput(bool isEnabled)
        {
            foreach (var behaviour in _inputBehaviours)
                behaviour.enabled = isEnabled;
        }
    }
}