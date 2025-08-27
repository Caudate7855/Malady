using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "LoadingScreen")]
    public class LoadingOverlayController : PanelControllerBase<LoadingOverlayView>
    {
        [Inject] private IPanelManager _panelManager;
        
        private FaderController _faderController;
        
        private GameObject _view;
        private TMP_Text _text;
        private Image _background;
        private Image _textBackground;
        
        protected override void Initialize()
        {
            _view = Panel.gameObject;
            _background = Panel.Background;
            _text = Panel.Text;
            _textBackground = Panel.TextBackground;

            _faderController = _panelManager.LoadPanel<FaderController>();
            
            InitializeView();
        }

        private void InitializeView()
        {
            InitializeText();
            InitializeBackground();
        }

        private void InitializeText()
        {
            
        }

        private void InitializeBackground()
        {
            
        }
    }
}