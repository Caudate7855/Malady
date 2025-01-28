using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class UIInstaller : MonoInstaller<UIInstaller>
    {
        [SerializeField] private SpellTip _spellTip;
            
        public override void InstallBindings()
        {
            Container
                .Bind<SpellTip>()
                .FromInstance(_spellTip)
                .AsSingle();

            Container
                .Bind<SpellTipHandler>()
                .AsSingle();
        }
    }
}