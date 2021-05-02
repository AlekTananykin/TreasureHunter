using Assets.Code.Exceptions;
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

        private uint _piratesCount = 1200;
        void Start()
        {
            try
            {
                _gameModeFabric = new GameModelFabric();
                _gameModel = _gameModeFabric.InitGameModel(_piratesCount);

                _controllersStorage = new ControllersStorage();
                _playerInput = new PlayerPcInput();
                new InitGame(_controllersStorage, _gameModel, _playerInput);

                _controllersStorage.Initialize();
            }
            catch (GameException ex)
            {
                Debug.LogError(ex.Message);
            }
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