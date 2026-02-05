using Project.Scripts.Configs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SpellTip : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private Image _resource1Image;
        [SerializeField] private Image _resource2Image;
        
        [SerializeField] private TMP_Text _resource1;
        [SerializeField] private TMP_Text _resource2;

        [Title("Images", bold: true)]
        [SerializeField] private Sprite _essenceResourceImage;

        [SerializeField] private Sprite _bonesResourceImage;
        [SerializeField] private Sprite _bloodResourceImage;
        [SerializeField] private Sprite _fleshResourceImage;
        [SerializeField] private Sprite _soulResourceImage;

        public void SetInfo(SpellConfig spellConfig)
        {
            _image.sprite = spellConfig.Icon;
            _name.text = spellConfig.Name;
            _description.text = spellConfig.Description;

            _resource1.text = spellConfig.Cost[ResourceType.Essence].ToString();
            _resource2.text = spellConfig.Cost[ResourceType.Blood].ToString();
        }
    }
}