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

        [SerializeField] private SummonSpellButton _firstSummonSpellButton;
        [SerializeField] private SummonSpellButton _secondSummonSpellButton;
        [SerializeField] private SummonSpellButton _thirdSummonSpellButton;
        [SerializeField] private SummonSpellButton _fourthSummonSpellButton;
        
        [SerializeField] private PlayerSpellButton _firstPlayerSpellButton;
        [SerializeField] private PlayerSpellButton _secondPlayerSpellButton;
        [SerializeField] private PlayerSpellButton _thirdPlayerSpellButton;
        [SerializeField] private PlayerSpellButton _fourthPlayerSpellButton;
        
        public HpBar HpSlider =>_hpSlider;
        public EssenceBar EssenceSlider =>_essenceSlider;
        
        public BonesBar BoneSlider => _boneSlider;
        public SoulBar SoulSlider => _soulSlider;
        public FleshBar FleshSlider => _fleshSlider;
        public BloodBar BloodSlider => _bloodSlider;

        public SummonSpellButton FirstSummonSpellButton => _firstSummonSpellButton;
        public SummonSpellButton SecondSummonSpellButton => _secondSummonSpellButton;
        public SummonSpellButton ThirdSummonSpellButton => _thirdSummonSpellButton;
        public SummonSpellButton FourthSummonSpellButton => _fourthSummonSpellButton;
        
        public PlayerSpellButton FirstPlayerSpellButton => _firstPlayerSpellButton;
        public PlayerSpellButton SecondPlayerSpellButton => _secondPlayerSpellButton;
        public PlayerSpellButton ThirdPlayerSpellButton => _thirdPlayerSpellButton;
        public PlayerSpellButton FourthPlayerSpellButton => _fourthPlayerSpellButton;
    }
}