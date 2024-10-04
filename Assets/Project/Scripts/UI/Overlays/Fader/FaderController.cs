using DG.Tweening;
using Itibsoft.PanelManager;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "Fader")]
    public class FaderController : PanelControllerBase<FaderOverlayView>
    {
        private const float FADING_DURATION = 3f;
        private Image _overlayImage;
        
        protected override void Initialize()
        {
            _overlayImage = Panel.OverlayImage;
        }

        public async void FadeIn()
        {
            await _overlayImage.DOFade(1, FADING_DURATION).AsyncWaitForCompletion();
        }
        
        public async void FadeOut()
        {
            await _overlayImage.DOFade(0, FADING_DURATION).AsyncWaitForCompletion();
            
            Close();
        }
    }
}