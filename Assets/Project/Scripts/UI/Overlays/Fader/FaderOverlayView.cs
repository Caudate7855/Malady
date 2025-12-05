using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class FaderOverlayView : PanelBase
    {
        [SerializeField] private Image _overlayImage;
        public Image OverlayImage => _overlayImage;
    }
}