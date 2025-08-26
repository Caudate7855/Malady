using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Overlays
{
    public class MainUIView : PanelBase
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private EssenceBar _essenceBar;

        [SerializeField] private BossBar _bossBar;
        
        [SerializeField] private List<ElementalBarBase> _elementalSliders;

        
        [SerializeField] private SpellList _summonSpellList;
        [SerializeField] private SpellList _playerSpellList;
        
        public HpBar HpBar =>_hpBar;
        public EssenceBar EssenceBar =>_essenceBar;
        public BossBar BossBar => _bossBar;
        public List<ElementalBarBase> ElementalBars => _elementalSliders;
        
        public SpellList SummonSpellList => _summonSpellList;
        public SpellList PlayerSpellList => _playerSpellList;
    }
}