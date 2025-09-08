using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = "SOContainersInstaller", menuName = "Installers/SOContainersInstaller")]
    public class SOContainersInstaller : ScriptableObjectInstaller<SOContainersInstaller>
    {
        [SerializeField] private SpellsContainerSo _spellsContainerSo;
        [FormerlySerializedAs("_spellModificatorsList")] [SerializeField] private SpellModificatorsConfigsContainer _spellModificatorsConfigsContainer;
            
        public override void InstallBindings()
        {
            Container
                .Bind<SpellsContainerSo>()
                .FromInstance(_spellsContainerSo)
                .AsSingle();

            Container
                .Bind<SpellModificatorsConfigsContainer>()
                .FromInstance(_spellModificatorsConfigsContainer)
                .AsSingle();
        }
    }
}