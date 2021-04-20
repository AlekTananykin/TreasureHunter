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

        void Start()
        {
            InitGameModel(out _gameModel);

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

        void InitGameModel(out GameModel model)
        {
            model = new GameModel();

            model.Hero = new HeroModel();
            model.Hero.Health = 100;
            model.Hero.MaxHealth = 100;
            model.Hero.InitPosition = new Vector3(10, 3, 10);
            model.Hero.Skill = 20;
            model.Hero.Speed = 5;

            model.Camera = new CameraModel();
            model.Camera.Forward = Vector3.down;
            model.Camera.Height = 30f;
            model.Camera.InitPosition = new Vector3(
                model.Hero.InitPosition.x,
                model.Camera.Height, model.Hero.InitPosition.z);
            model.Camera.Speed = 10f;
        }
    }
}