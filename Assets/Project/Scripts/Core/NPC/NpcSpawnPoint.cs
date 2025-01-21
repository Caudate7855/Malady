using UnityEngine;

namespace Project.Scripts.Core
{
    public class NpcSpawnPoint : MonoBehaviour
    {
        [SerializeField] private NpcTypes _npcType;

        public NpcTypes GetSpawnPointType()
        {
            return _npcType;
        }
    }
}