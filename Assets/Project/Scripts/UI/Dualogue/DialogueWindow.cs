using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class DialogueWindow : PanelBase
    {
        public TMP_Text Text => _text;
        public GameObject TextWindow => _textWindow;
        public TMP_Text NpcName => _npcName;
        public Vector2 StartPosition => _startPosition;
        public Vector2 EndPosition => _endPosition;
        
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _textWindow;
        [SerializeField] private TMP_Text _npcName;

        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;
    }
}