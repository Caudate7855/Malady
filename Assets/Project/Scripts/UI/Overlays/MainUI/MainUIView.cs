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

        
        [SerializeField] private SpellList _summonSpellList;
        [SerializeField] private SpellList _playerSpellList;
        
        public HpBar HpSlider =>_hpSlider;
        public EssenceBar EssenceSlider =>_essenceSlider;
        public BossBar BossBar => _bossBar;
        public List<ElementalBarBase> ElementalBars => _elementalSliders;
        
        public SpellList SummonSpellList => _summonSpellList;
        public SpellList PlayerSpellList => _playerSpellList;
    }
}