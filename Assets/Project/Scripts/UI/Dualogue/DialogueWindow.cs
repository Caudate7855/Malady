using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class DialogueWindow : PanelBase
    {
        public TMP_Text Text => _text;
        public GameObject TextWindow => _textWindow;
        public GameObject ImageWindow => _imageWindow;
        public TMP_Text NpcName => _npcName;
        public Image SpeakerImage => _speakerImage;
        public Vector2 StartTextPosition => _startTextPosition;
        public Vector2 EndTextPosition => _endTextPosition;
        public Vector2 StartImagePosition => _startImagePosition;
        public Vector2 EndImagePosition => _endImagePosition;
        
        public Button DialogueChangeButton => _dialogueChangeButton;
        
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _textWindow;
        [SerializeField] private GameObject _imageWindow;
        [SerializeField] private TMP_Text _npcName;
        [SerializeField] private Image _speakerImage;

        [SerializeField] private Vector2 _startTextPosition;
        [SerializeField] private Vector2 _endTextPosition;

        [SerializeField] private Vector2 _startImagePosition;
        [SerializeField] private Vector2 _endImagePosition;

        [SerializeField] private Button _dialogueChangeButton;
    }
}