using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using Assets.Code.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private ControllersStorage _controllersStorage;
        private GameModel _gameModel;
        private IPlayerInput _playerInput;
        private GameModelFabric _gameModeFabric;
        private IGameSaver<GameModel> _gameSaver;
        private const string _savedGamesPath = "SavedGames";

        private uint _piratesCount = 1200;
        void Start()
        {
            try
            {
                _gameSaver = new GameSaver<GameModel>(_savedGamesPath);

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
                Application.Quit();
            }
        }

        void Update()
        {
            if (_playerInput.IsSave())
            {
                _gameSaver.Save(_gameModel);
                Debug.Log("Game is saved. ");
            }
            if (_playerInput.IsLoad())
            {
                IList<string> filenames = _gameSaver.GetSaveList();
                foreach (string filename in filenames)
                    Debug.Log(filename);

                _gameSaver.Load(filenames.Count - 1, out _gameModel);

                Debug.Log("Game is loaded. But model hasn't been applied. ");
            }

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