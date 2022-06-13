using UnityEngine;

namespace poetools
{
    // todo: clean up and make better
    
    public class DebugSettings : MonoBehaviour
    {
        [SerializeField] 
        private bool showFPS;

        [SerializeField] 
        private bool quitOnEscape;

        [SerializeField] 
        private bool hideCursor;

        private void Update()
        {
            UpdateQuitOnEscape();
            UpdateHideCursor();
            UpdateShowFPS();
        }

        private void UpdateQuitOnEscape()
        {
            if (quitOnEscape && Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private void UpdateHideCursor()
        {
            Cursor.visible = !hideCursor;
            Cursor.lockState = hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void UpdateShowFPS()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }
        
        private GUIStyle _style;
        private float _deltaTime;

        private void Awake()
        {
            _style = GUIStyle.none;
            _style.alignment = TextAnchor.UpperRight;
            _style.fontSize = Screen.height * 2 / 100;
            _style.normal.textColor = Color.white;
        }

        private void OnGUI()
        {
            if (!showFPS)
                return;
            
            Rect rect = new Rect(0, 0, Screen.width, _style.fontSize);
            string framerate = CreateFramerateMessage();
            
            GUI.Label(rect, framerate, _style);
        }

        private string CreateFramerateMessage()
        {
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            return $"{msec:0.0} ms ({fps:0.} fps)";
        }
    }
}