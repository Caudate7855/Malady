using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = "SOContainersInstaller", menuName = "Installers/SOContainersInstaller")]
    public class SOContainersInstaller : ScriptableObjectInstaller<SOContainersInstaller>
    {
        [SerializeField] private SpellsSpriteContainerSO _spellSpriteContainerSO;
            
        public override void InstallBindings()
        {
            Container
                .Bind<SpellsSpriteContainerSO>()
                .FromInstance(_spellSpriteContainerSO)
                .AsSingle();
        }
    }
}