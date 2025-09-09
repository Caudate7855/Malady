using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class PlayerSpellModificatorsSystem
    {
        [Inject] private SpellModificatorsConfigsContainer _spellModificatorsConfigsContainer;
        private List<SpellModificatorBase> _spellModificators = new();

        public void AddModificator(SpellModificatorBase newModificator)
        {
            if (_spellModificators.Contains(newModificator))
            {
                return;
            }

            var spellConfig = _spellModificatorsConfigsContainer.GetModificatorById(newModificator.ID);
            newModificator.Description = spellConfig.Description;
            
            _spellModificators.Add(newModificator);
        }

        public void RemoveModificator<T>() where T : SpellModificatorBase
        {
            for (int i = 0, count = _spellModificators.Count; i < count; i++)
            {
                if (_spellModificators[i] is T)
                {
                    _spellModificators.RemoveAt(i);
                }
            }
        }

        public T GetModificatorByType<T>() where T : SpellModificatorBase
        {
            for (int i = 0, count = _spellModificators.Count; i < count; i++)
            {
                if (_spellModificators[i] is T)
                {
                    return _spellModificators[i] as T;
                }
            }

            return null;
        }
        
        public SpellModificatorBase GetModificatorByID(string id)
        {
            for (int i = 0, count = _spellModificators.Count; i < count; i++)
            {
                if (_spellModificators[i].ID == id)
                {
                    return _spellModificators[i];
                }
            }

            return null;
        }
    }
}