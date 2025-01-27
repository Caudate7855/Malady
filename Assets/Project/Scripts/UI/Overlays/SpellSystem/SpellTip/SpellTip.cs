using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class SpellTip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        
        [SerializeField] private Image _image;

        [SerializeField] private Image _resourceTypeImage;
        [SerializeField] private Image _resourceTypeImage2;
        [SerializeField] private TMP_Text _resourcesCost;
        [SerializeField] private TMP_Text _resourcesCost2;
    }
}