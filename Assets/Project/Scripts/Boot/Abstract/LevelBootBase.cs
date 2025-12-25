using System;
using Cysharp.Threading.Tasks;
using DunGen.DungeonCrawler;
using Itibsoft.PanelManager;
using Project.Scripts.Services;
using R3;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class LevelBootBase : MonoBehaviour, IDisposable, IInitializable
    {
        [SerializeField] private CameraFollower _mainCamera;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        [Inject] protected GlobalFactory GlobalFactory;
        [Inject] protected IPanelManager PanelManager;
        [Inject] private InputController _inputController;
        [Inject] protected PlayerController PlayerController;
        
        protected NavMeshAgent PlayerNavMeshAgent;

        private readonly Vector3 _playerPosition = new(0, 0, 0);
        private CoreUpdater _coreUpdater;
        private CompositeDisposable _compositeDisposable = new();

        public virtual void Initialize()
        {
            PlayerNavMeshAgent = PlayerController.GetComponent<NavMeshAgent>();
            PlayerNavMeshAgent.enabled = false;
        }

        private async void Start()
        {
            PlayerController.gameObject.transform.position = _playerPosition;
            Initialize();

            await FinishLoading();

            PanelManager.LoadPanel<MainUIController>().Open();
        }

        private void OnEnable()
        {
            _coreUpdater = FindFirstObjectByType<CoreUpdater>();
            
            _coreUpdater.OnUpdatePerformed
                .Subscribe(_ =>_inputController.Update())
                .AddTo(_compositeDisposable);
        }

        private async UniTask FinishLoading()
        {
            var controller = PanelManager.LoadPanel<LoadingOverlayController>();
            var fader = PanelManager.LoadPanel<FaderController>();

            fader.Open();
            fader.FadeIn();

            await UniTask.Delay((int)FaderController.FadeDuration * 1000);

            controller.Close();
        }
        
        protected async UniTask BuildNavMeshAndSpawnAsync()
        {
            await UniTask.Yield();

            _navMeshSurface.BuildNavMesh();

            await UniTask.Yield();

            var spawn = FindFirstObjectByType<PlayerSpawn>();
            
            if (spawn == null)
            {
                Debug.LogError("PlayerSpawn not found in scene");
                return;
            }

            PlayerNavMeshAgent.Warp(spawn.transform.position);
            PlayerNavMeshAgent.enabled = true;
        }

        public virtual void Dispose()
        {
            _compositeDisposable.Dispose();   
        }
    }
}