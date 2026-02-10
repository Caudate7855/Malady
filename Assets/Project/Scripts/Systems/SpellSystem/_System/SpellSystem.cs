using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public sealed class SpellSystem : ITickable
    {
        public List<ISpell> ChosenSpells = new();

        private readonly DiContainer _container;
        private readonly SpellsConfig _spellsConfig;
        private readonly List<Type> _global = new();
        private readonly Dictionary<Type, List<Type>> _perSpell = new();
        private readonly List<ISpell> _activeSpells = new();
        private readonly Dictionary<ISpell, List<ISpellModifier>> _runtimeModifs = new();
        private readonly Transform _playerOrigin;

        public SpellSystem(DiContainer container, SpellsConfig spellsConfig, PlayerController playerController)
        {
            _container = container;
            _spellsConfig = spellsConfig;
            _playerOrigin = playerController.transform;
        }

        public void SetChosenSpellByIndex(int index, SpellBase spellBase)
        {
            if (index < 0)
            {
                throw new Exception("index < 0");
            }
            
            EnsureChosenSize(index + 1);

            if (spellBase == null)
            {
                ChosenSpells[index] = default;
                return;
            }

            var spellType = spellBase.GetType();

            if (typeof(ISpell).IsAssignableFrom(spellType) == false)
            {
                throw new Exception($"SpellBase type {spellType.Name} does not implement ISpell");
            }

            var spell = CreateSpellInstance(spellType);

            ChosenSpells[index] = spell;
        }

        public void CastPlayerSpellByIndex(int index)
        {
            if (index < 0)
            {
                throw new Exception("index < 0");
            }

            if (ChosenSpells == null || ChosenSpells.Count == 0)
            {
                return;
            }

            if (index >= ChosenSpells.Count)
            {
                return;
            }

            var chosen = ChosenSpells[index];
            if (chosen == null)
            {
                return;
            }

            if (_playerOrigin == null)
            {
                throw new Exception("SpellSystem: player origin is null");
            }

            CastSpellByType(chosen.GetType(), _playerOrigin);
        }

        public ISpell CastSpellByType(Type spellType, Transform origin)
        {
            if (spellType == null)
            {
                throw new Exception("spellType is null");
            }

            if (origin == null)
            {
                throw new Exception("origin is null");
            }

            if (typeof(SpellBase).IsAssignableFrom(spellType) == false)
            {
                throw new Exception($"Type {spellType.Name} is not SpellBase");
            }

            var config = _spellsConfig.GetSpellConfig(spellType);
            var view = config.SpellView;
            if (view == null)
            {
                throw new Exception($"SpellView is null for {spellType.Name}");
            }

            var prefab = view.gameObject;

            var spellObj = _container.Instantiate(spellType, new object[] { prefab, config });

            if (spellObj is ISpell spell)
            {
                CastSpellInternal(spell, origin);
                return spell;
            }

            throw new Exception($"Instantiated spell {spellType.Name} is not ISpell");
        }

        private void EnsureChosenSize(int size)
        {
            while (ChosenSpells.Count < size)
            {
                ChosenSpells.Add(default);
            }
        }

        private ISpell CreateSpellInstance(Type spellType)
        {
            var config = _spellsConfig.GetSpellConfig(spellType);

            var view = config.SpellView;
            if (view == null)
            {
                throw new Exception($"SpellView is null for {spellType.Name}");
            }

            var prefab = view.gameObject;

            var spellObj = _container.Instantiate(spellType, new object[] { prefab, config });

            if (spellObj is ISpell spell)
            {
                return spell;
            }

            throw new Exception($"Instantiated spell {spellType.Name} is not ISpell");
        }

        public void AddFor<TSpell, TModif>() where TSpell : ISpell where TModif : ISpellModifier
        {
            var key = typeof(TSpell);

            if (!_perSpell.TryGetValue(key, out var list))
            {
                list = new List<Type>();
                _perSpell.Add(key, list);
            }

            list.Add(typeof(TModif));
        }

        public int RemoveFor<TSpell, TModif>()
            where TSpell : ISpell
            where TModif : ISpellModifier
        {
            var spellType = typeof(TSpell);

            if (!_perSpell.TryGetValue(spellType, out var list))
                return 0;

            var modType = typeof(TModif);
            var removed = 0;

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] != modType) continue;
                list.RemoveAt(i);
                removed++;
            }

            if (list.Count == 0)
                _perSpell.Remove(spellType);

            return removed;
        }

        public void AddGlobal<TModif>() where TModif : ISpellModifier
        {
            _global.Add(typeof(TModif));
        }

        public int RemoveGlobal<TModif>() where TModif : ISpellModifier
        {
            var t = typeof(TModif);
            var removed = 0;

            for (var i = _global.Count - 1; i >= 0; i--)
            {
                if (_global[i] != t) continue;
                _global.RemoveAt(i);
                removed++;
            }

            return removed;
        }

        public void Tick()
        {
            Tick(Time.deltaTime);
        }

        private void Tick(float dt)
        {
            for (var i = _activeSpells.Count - 1; i >= 0; i--)
            {
                var spell = _activeSpells[i];

                spell.Tick(dt);

                if (spell.ActiveCount != 0)
                    continue;

                if (_runtimeModifs.TryGetValue(spell, out var mods))
                {
                    for (var m = 0; m < mods.Count; m++)
                        mods[m].Dispose();

                    _runtimeModifs.Remove(spell);
                }

                _activeSpells.RemoveAt(i);
            }
        }

        public TSpell CastSpell<TSpell>(Transform origin) where TSpell : SpellBase
        {
            var config = _spellsConfig.GetSpellConfig(typeof(TSpell));
            var view = config.SpellView;
            var prefab = view.gameObject;

            var spell = _container.Instantiate<TSpell>(new object[] { prefab, config });

            CastSpellInternal(spell, origin);

            return spell;
        }

        private void CastSpellInternal(ISpell spell, Transform origin)
        {
            spell.Origin = origin;
            var mods = CreateModifsFor(spell);

            ApplyAll(spell, mods);

            _runtimeModifs[spell] = mods;
            _activeSpells.Add(spell);

            spell.Cast();
        }

        private List<ISpellModifier> CreateModifsFor(ISpell spell)
        {
            var types = new List<Type>(_global.Count + 8);

            for (var i = 0; i < _global.Count; i++)
            {
                types.Add(_global[i]);
            }

            var spellType = spell.GetType();

            if (_perSpell.TryGetValue(spellType, out var list))
            {
                for (var i = 0; i < list.Count; i++)
                    types.Add(list[i]);
            }

            var mods = new List<ISpellModifier>(types.Count);

            for (var i = 0; i < types.Count; i++)
            {
                mods.Add((ISpellModifier)_container.Instantiate(types[i]));
            }

            mods.Sort((a, b) =>
            {
                var pa = a is IPriority ap ? ap.Priority : 0;
                var pb = b is IPriority bp ? bp.Priority : 0;
                return pa.CompareTo(pb);
            });

            return mods;
        }

        private void ApplyAll(ISpell spell, List<ISpellModifier> mods)
        {
            for (var i = 0; i < mods.Count; i++)
            {
                mods[i].Apply(spell);
            }
        }
    }
}
