using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.Overlays
{
    public class MainUIView : PanelBase
    {
        [SerializeField] private HpBar _hpSlider;
        [SerializeField] private EssenceBar _essenceSlider;

        [SerializeField] private BossBar _bossBar;
        
        [SerializeField] private List<ElementalBarBase> _elementalSliders;

        [SerializeField] private List<SummonSpellButton> _summonSpellsButtons;
        [SerializeField] private List<PlayerSpellButton> _playerSpellsButtons;
        
        public HpBar HpSlider =>_hpSlider;
        public EssenceBar EssenceSlider =>_essenceSlider;
        public BossBar BossBar => _bossBar;
        public List<ElementalBarBase> ElementalBars => _elementalSliders;
        public List<SummonSpellButton> SummonSpellsButtons => _summonSpellsButtons;
        public List<PlayerSpellButton> PlayerSpellButton => _playerSpellsButtons;
    }
}