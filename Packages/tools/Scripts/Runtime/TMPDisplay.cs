using TMPro;
using UnityEngine;
using Display = UnityTemplateProjects.UI.Display;

namespace UI
{
    public class TMPDisplay : Display
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public override void UpdateText(string value)
        {
            if (Application.isPlaying == false)
                _text = GetComponent<TMP_Text>();
            
            _text.text = value;
        }
    }
}