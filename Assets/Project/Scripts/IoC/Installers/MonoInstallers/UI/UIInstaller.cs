using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class UIInstaller : MonoInstaller<UIInstaller>
    {
        [SerializeField] private SpellTip _spellTip;
        [SerializeField] private ItemTip _itemTip;
        
        public override void InstallBindings()
        {
            Container
                .Bind<SpellTip>()
                .FromInstance(_spellTip)
                .AsSingle();
            
            Container
                .Bind<ItemTip>()
                .FromInstance(_itemTip)
                .AsSingle();
        }
    }
}