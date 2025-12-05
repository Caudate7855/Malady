using DG.Tweening;
using Itibsoft.PanelManager;
using UnityEngine.UI;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "Fader")]
    public class FaderController : PanelControllerBase<FaderOverlayView>
    {
        public const float FadeDuration = 2f;
        private Image _overlayImage;
        
        protected override void Initialize()
        {
            _overlayImage = Panel.OverlayImage;
        }

        public async void FadeIn()
        {
            await _overlayImage.DOFade(1, FadeDuration).AsyncWaitForCompletion();
            await _overlayImage.DOFade(0, FadeDuration).AsyncWaitForCompletion();
            
            Close();
        }
    }
}