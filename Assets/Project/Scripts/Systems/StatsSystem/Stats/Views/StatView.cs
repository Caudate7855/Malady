using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts
{
    public class StatView : MonoBehaviour
    {
        [FormerlySerializedAs("StatType")] public StatType Type;
        
        public TMP_Text NameText => _nameText;
        public TMP_Text ValueText => _valueText;
        
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;
    }
}