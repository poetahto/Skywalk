using System.Collections;
using UnityEngine;
using Yarn.Unity;

namespace Game.Scripts
{
    public class DialogBehaviour : MonoBehaviour
    {
        [SerializeField] private string startNode;
        [SerializeField] private float cooldown = 1f;

        private DialogueRunner _dialogueRunner;
        private bool _isLocked;

        private void Start()
        {
            _dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        public void PrintDialogue()
        {
            if (!_isLocked)
            {
                _dialogueRunner.StartDialogue(startNode);
                StartCoroutine(CooldownCoroutine());
            }
        }

        private IEnumerator CooldownCoroutine()
        {
            _isLocked = true;
            
            yield return new WaitUntil(() => !_dialogueRunner.IsDialogueRunning);
            yield return new WaitForSeconds(cooldown);

            _isLocked = false;
        }
    }
}