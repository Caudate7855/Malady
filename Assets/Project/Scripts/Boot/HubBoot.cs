using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : LevelBootBase
    {
        [Inject] private HubFactory _hubFactory;
        [Inject] private BookInteractable _book;
        
        protected override async void Initialize()
        {
            await _hubFactory.Create<HubController>();
            _book = Instantiate(_book, new Vector3(4f,0.5f,5f), quaternion.identity);
        }
    }
}