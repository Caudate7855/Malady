using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Markers;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class InteractableFactory
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BookInteractable _bookInteractable;

        public void CreateBook(GameObject parentObject)
        {
            _diContainer.InstantiatePrefabForComponent<BookInteractable>(_bookInteractable, parentObject.transform);
        }
    }
}