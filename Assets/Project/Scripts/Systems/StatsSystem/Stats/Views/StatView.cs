using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class StatView : MonoBehaviour
    {
        public TMP_Text NameText => _nameText;
        public TMP_Text ValueText => _valueText;
        
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;
    }
}