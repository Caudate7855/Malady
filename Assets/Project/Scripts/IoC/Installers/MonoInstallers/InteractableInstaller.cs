using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class InteractableInstaller : MonoInstaller<InteractableInstaller>
    {
        [SerializeField] private BookInteractable _bookInteractable;
        [SerializeField] private ExitInteractable _exitInteractable;

        public override void InstallBindings()
        {
            Container
                .Bind<BookInteractable>()
                .FromInstance(_bookInteractable)
                .AsSingle();
            
            Container
                .Bind<ExitInteractable>()
                .FromInstance(_exitInteractable)
                .AsSingle();
        }
    }
}