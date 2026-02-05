using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SpellTipResource : MonoBehaviour
    {
        [SerializeField] private Image _resourceIcon;
        [SerializeField] private TMP_Text _resourceCost;

        public void SetInfo(Sprite sprite, float cost)
        {
            _resourceIcon.sprite = sprite;
            _resourceCost.text = cost.ToString(CultureInfo.InvariantCulture);
        }
    }
}