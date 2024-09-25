using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class LoadingOverlayView : PanelBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;
    }
}