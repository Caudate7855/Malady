using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Overlays
{
    public class MainUIView : PanelBase
    {
        [SerializeField] private HpBar _hpSlider;
        [SerializeField] private EssenceBar _essenceSlider;
        
        [SerializeField] private BonesBar _boneSlider;
        [SerializeField] private SoulBar _soulSlider;
        [SerializeField] private FleshBar _fleshSlider;
        [SerializeField] private BloodBar _bloodSlider;

        [SerializeField] private Button _firstSummonSpellButton;
        [SerializeField] private Button _secondSummonSpellButton;
        [SerializeField] private Button _thirdSummonSpellButton;
        [SerializeField] private Button _fourthSummonSpellButton;
        
        [SerializeField] private Button _firstPlayerSpellButton;
        [SerializeField] private Button _secondPlayerSpellButton;
        [SerializeField] private Button _thirdPlayerSpellButton;
        [SerializeField] private Button _fourthPlayerSpellButton;
    }
}