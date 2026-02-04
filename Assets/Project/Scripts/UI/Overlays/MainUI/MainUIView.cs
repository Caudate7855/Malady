using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts
{
    public class MainUIView : PanelBase
    {
        [field: SerializeField] public HpBar HpBar;
        [field: SerializeField] public EssenceBar EssenceBar;
        [field: SerializeField] public BossBar BossBar;
        [field: SerializeField] public List<ElementalBarBase> ElementsSliders;
        [field: SerializeField] public List<SpellUIButton> SpellUIButtons;
    }
}