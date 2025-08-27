using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class LoadingOverlayView : PanelBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;
        [SerializeField] private Image _textBackground;
        
        public TMP_Text Text => _text;
        public Image Background => _background;
        public Image TextBackground => _textBackground;
    }
}