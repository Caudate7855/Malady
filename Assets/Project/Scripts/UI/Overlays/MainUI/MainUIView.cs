using System.Collections.Generic;
using Itibsoft.PanelManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts
{
    public class MainUIView : PanelBase
    {
        [Title("Bars", horizontalLine: false, bold: true)]
        [field: SerializeField] public HpBar HpBar;
        [field: SerializeField] public EssenceBar EssenceBar;
        [field: SerializeField] public BossBar BossBar;
        
        [Space]
        [Title("Resource bars", horizontalLine: false, bold: true)]
        [field: SerializeField] public BonesBar BonesBar;
        [field: SerializeField] public BloodBar BloodBar;
        [field: SerializeField] public FleshBar FleshBar;
        [field: SerializeField] public SoulBar SoulBar;
        
        [Space]
        [Title("Spell buttons", horizontalLine: false, bold: true)]
        [field: SerializeField] public List<DragAndDropSlot> SpellUISlots;
    }
}