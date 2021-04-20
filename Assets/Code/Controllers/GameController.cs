using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private ControllersStorage _controllersStorage;
        private GameModel _gameModel;
        private IPlayerInput _playerInput;
        private GameModelFabric _gameModeFabric;
        void Start()
        {
            _gameModeFabric = new GameModelFabric();
            _gameModeFabric.InitGameModel(out _gameModel);

            _controllersStorage = new ControllersStorage();
            _playerInput = new PlayerPcInput();
            new InitGame(_controllersStorage, _gameModel, _playerInput);

            _controllersStorage.Initialize();
        }

        void Update()
        {
            _controllersStorage.Execute(Time.deltaTime);
        }
        private void LateUpdate()
        {
            _controllersStorage.LateExecute(Time.deltaTime);
        }
        public void OnDestroy()
        {
            _controllersStorage.Cleanup();
        }
    }
}