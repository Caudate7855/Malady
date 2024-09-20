using System.Collections.Generic;
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

        [SerializeField] private List<SummonSpellButton> _summonSpellsButtons;
        [SerializeField] private List<PlayerSpellButton> _playerSpellsButtons;
        
        public HpBar HpSlider =>_hpSlider;
        public EssenceBar EssenceSlider =>_essenceSlider;
        
        public BonesBar BoneSlider => _boneSlider;
        public SoulBar SoulSlider => _soulSlider;
        public FleshBar FleshSlider => _fleshSlider;
        public BloodBar BloodSlider => _bloodSlider;

        public List<SummonSpellButton> SummonSpellsButtons => _summonSpellsButtons;
        public List<PlayerSpellButton> PlayerSpellButton => _playerSpellsButtons;
    }
}