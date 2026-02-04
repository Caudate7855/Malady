using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts
{
    public class MainUIView : PanelBase
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private EssenceBar _essenceBar;

        [SerializeField] private BossBar _bossBar;
        
        [SerializeField] private List<ElementalBarBase> _elementalSliders;

        public HpBar HpBar =>_hpBar;
        public EssenceBar EssenceBar =>_essenceBar;
        public BossBar BossBar => _bossBar;
        public List<ElementalBarBase> ElementalBars => _elementalSliders;
    }
}