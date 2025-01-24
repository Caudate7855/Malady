using DG.Tweening;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "DialogueWindow")]
    public class DialogueWindowController : PanelControllerBase<DialogueWindow>
    {
        private const float MOVE_DURATION = 0.8f;
        private const float FADE_DURATION = 0.8f;
        
        public NpcTypes CurrentNpcType;
        
        [Inject] private DialogueSystemManager _dialogueSystemManager;

        private TMP_Text _text;
        private TMP_Text _npcName;
        private Image _speakerImage;
        
        private GameObject _textWindow;
        private GameObject _imageWindow;

        private RectTransform _textRectTransform;
        private RectTransform _imageRectTransform;
        
        private Vector2 _startTextPosition;
        private Vector2 _endTextPosition;
        
        private Vector2 _startImagePosition;
        private Vector2 _endImagePosition;

        private Button _dialogueChangeButton;
        
        private bool _isImageShowing;
        
        protected override void Initialize()
        {
            _text = Panel.Text;
            _npcName = Panel.NpcName;
            _speakerImage = Panel.SpeakerImage;

            _startTextPosition = Panel.StartTextPosition;
            _endTextPosition = Panel.EndTextPosition;
            _startImagePosition = Panel.StartImagePosition;
            _endImagePosition = Panel.EndImagePosition;
            
            _dialogueChangeButton = Panel.DialogueChangeButton;
            
            _textWindow = Panel.TextWindow;
            _imageWindow = Panel.ImageWindow;

            _dialogueChangeButton.onClick.AddListener(IncreaseDialogueStep);

            _textRectTransform = _textWindow.GetComponent<RectTransform>();
            _imageRectTransform = _imageWindow.GetComponent<RectTransform>();
        }

        public void ShowDialogueWindow()
        {
            if (_dialogueSystemManager.GetCurrentDialogue(CurrentNpcType) == _dialogueSystemManager.EndDialogueKey)
            {
                HideDialogueWindow();
                return;
            }

            ShowDialogueText();
            ShowSpeakerImage();
        }

        private void ShowDialogueText()
        {
            _text.text = _dialogueSystemManager.GetCurrentDialogue(CurrentNpcType);
            _npcName.text = _dialogueSystemManager.GetNpcName(CurrentNpcType);

            _textRectTransform.anchoredPosition = _startTextPosition;
             
             var color = _text.color;
             color.a = 0;
             _text.color = color;
             
             var sequence = DOTween.Sequence();
             
             sequence.
                 Join(_textRectTransform.DOAnchorPos(_endTextPosition, MOVE_DURATION).SetEase(Ease.Linear))
                 .Join(_text.DOFade(1f, FADE_DURATION));
        }

        private void ShowSpeakerImage()
        {
            if (_isImageShowing)
            {
                return;
            }

            _isImageShowing = true;
            
            _speakerImage.sprite = _dialogueSystemManager.GetCurrentSpeakerSprite(CurrentNpcType);
            
            _imageRectTransform.anchoredPosition = _startImagePosition;
             
            var color = _speakerImage.color;
            color.a = 0;
            _speakerImage.color = color;
             
            var sequence = DOTween.Sequence();
            
            sequence.
                Join(_imageRectTransform.DOAnchorPos(_endImagePosition, MOVE_DURATION).SetEase(Ease.Linear))
                .Join(_speakerImage.DOFade(1f, FADE_DURATION));
        }

        private void HideDialogueWindow()
        {
            _isImageShowing = false;
            Close();
        }

        private void IncreaseDialogueStep()
        {
            _dialogueSystemManager.UpgradeDialogueState(CurrentNpcType);
            ShowDialogueWindow();
        }
    }
}