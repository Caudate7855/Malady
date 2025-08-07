using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellsLogicsList
    {
        private List<SpellBase> _spellList;

        [Inject]
        public void Construct(List<SpellBase> spells)
        {
            _spellList = spells;
            InitializeSpells();
        }

        private void InitializeSpells()
        {
            for (int i = 0, count = _spellList.Count; i < count; i++)
            {
                _spellList[i].Initialize();
            }
        }

        public void CastSpell(string id)
        {
            for (int i = 0, count = _spellList.Count; i < count; i++)
            {
                if (_spellList[i].ID == id)
                {
                    _spellList[i].Cast();
                }
            }
        }
    }
}