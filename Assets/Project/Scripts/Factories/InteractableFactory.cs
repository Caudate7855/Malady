using JetBrains.Annotations;
using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class InteractableFactory
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BookInteractable _bookInteractable;
        [Inject] private Exit _exit;

        public void CreateBook(GameObject parentObject)
        {
            _diContainer.InstantiatePrefabForComponent<BookInteractable>(_bookInteractable, parentObject.transform);
        }

        public void CreateExit(GameObject parentObject)
        {
            _diContainer.InstantiatePrefabForComponent<Exit>(_exit, parentObject.transform);
        }
    }
}