using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private bool _isSandBox;
        
        [Inject] private IPanelManager _panelManager;
        [Inject] private ISceneLoader _sceneLoader;

        private Fsm _gameStateFsm = new();

        private LoadingOverlayController _loadingOverlayController;

        private Dictionary<GameStateType, Type> _gameStates = new()
        {
            { GameStateType.MainMenu, typeof(MainMenuGameStateBase) },
            { GameStateType.SandBox, typeof(SandBoxGameState) },
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
            if (_isSandBox)
            {
                ChangeState(GameStateType.SandBox);
                return;
            }
            
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
            _gameStateFsm.AddState(new SandBoxGameState(_gameStateFsm, _sceneLoader));
            _gameStateFsm.AddState(new MainMenuGameStateBase(_gameStateFsm, _sceneLoader));
            _gameStateFsm.AddState(new HubGameState(_gameStateFsm, _sceneLoader));
            _gameStateFsm.AddState(new ChurchGameState(_gameStateFsm, _sceneLoader));

            
            if(_isSandBox)
            {
                _gameStateFsm.SetState<SandBoxGameState>();
            }
            else
            {
                _gameStateFsm.SetState<MainMenuGameStateBase>();
            }
        }
    }
}