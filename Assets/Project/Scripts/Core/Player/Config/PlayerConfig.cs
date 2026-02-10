using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : SerializedScriptableObject
    {
        [Title("Movement", bold: true, horizontalLine: false)]
        public float MoveSpeed;
        public float AngularSpeed;
        public float Acceleration;
        public float Rotation;
        
        [OdinSerialize]
        [Title("FSMStates", bold: true, horizontalLine: false)] 
        public Dictionary<PlayerCastAnimationType, PlayerFSMState> FSMStates { get; set; } = new();
        
        [OdinSerialize]
        [Title("Init", bold: true, horizontalLine: false)]
        public List<InitialSpells> InitialSpells { get; private set; } = new();
        
        [OdinSerialize] 
        public List<InitialStats> InitialStats { get; private set; } = new();

        public PlayerFSMStateBase GetPlayerFSMState()
        {
            var state = FSMStates.Values
                .Select(x => x.Spell)
                .FirstOrDefault(x => x != null);

            return state;
        }

        public ISpell GetInitialSpell()
        {
            var spell = InitialSpells
                .Select(x => x.Spell)
                .FirstOrDefault(x => x != null);

            return spell;
        }

        public IStat GetInitialStat()
        {
            var entry = InitialStats.FirstOrDefault(x => x.Stat != null);

            return entry.Stat;
        }

    }
    
    [Serializable]
    public struct PlayerFSMState
    {
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        [HideLabel]
        public PlayerFSMStateBase Spell;
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(PlayerFSMStateBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(PlayerFSMStateBase).IsAssignableFrom(x));

            return type;
        }
    }
    
    [Serializable]
    public struct InitialSpells
    {
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        [HideLabel]
        public ISpell Spell;
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(ISpell).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(ISpell).IsAssignableFrom(x));

            return type;
        }
    }
    
    [Serializable]
    public struct InitialStats
    {
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        [HideLabel]
        public IStat Stat;
        
        public float InitValue;
        public float MaxValue;
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(IStat).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(IStat).IsAssignableFrom(x));

            return type;
        }
    }
}