using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class Main : MonoBehaviour
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private ISceneLoader _sceneLoader;

        private Fsm _gameStateFsm = new();

        private LoadingOverlayController _loadingOverlayController;

        private Dictionary<GameStateType, Type> _gameStates = new()
        {
            { GameStateType.MainMenu, typeof(MainMenuGameStateBase) },
            { GameStateType.Hub, typeof(HubGameState) },
            { GameStateType.Church, typeof(ChurchGameState) }
        };

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            InitializeGameFsm();

            _loadingOverlayController = _panelManager.LoadPanel<LoadingOverlayController>();
        }

        private void Start()
        {
            ShowLoadingScreen();
            ChangeState(GameStateType.MainMenu);
        }

        public void ShowLoadingScreen()
        {
            _loadingOverlayController.Open();
        }

        public void ChangeState(GameStateType gameStateType)
        {
            ShowLoadingScreen();
            
            if (_gameStates.TryGetValue(gameStateType, out var gameState))
            {
                var method = _gameStateFsm.GetType().GetMethod("SetState");
                var genericMethod = method.MakeGenericMethod(gameState);
                genericMethod.Invoke(_gameStateFsm, null);
            }
        }

        private void InitializeGameFsm()
        {
            _gameStateFsm.AddState(new MainMenuGameStateBase(_gameStateFsm, _sceneLoader));
            _gameStateFsm.AddState(new HubGameState(_gameStateFsm, _sceneLoader));
            _gameStateFsm.AddState(new ChurchGameState(_gameStateFsm, _sceneLoader));

            _gameStateFsm.SetState<MainMenuGameStateBase>();
        }
    }
}