using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = "SOContainersInstaller", menuName = "Installers/SOContainersInstaller")]
    public class SOContainersInstaller : ScriptableObjectInstaller<SOContainersInstaller>
    {
        [SerializeField] private SpellsContainerSo _spellsContainerSo;
            
        public override void InstallBindings()
        {
            Container
                .Bind<SpellsContainerSo>()
                .FromInstance(_spellsContainerSo)
                .AsSingle();
        }
    }
}