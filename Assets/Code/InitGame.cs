using Assets.Code.Controllers;
using Assets.Code.Interfaces;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
using System;
using UnityEngine;

namespace Assets.Code
{
    sealed class InitGame
    {
        public InitGame(ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput)
        {
            ViewsFabric viewsFabric = new ViewsFabric();

            InitializeCameraAndPlayer(viewsFabric,
                controllers, model, playerInput);

            InitializePirates(controllers, model);
        }

        private void InitializeCameraAndPlayer(
            ViewsFabric viewsFabric,
            ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput)
        {
            GameObject heroView = viewsFabric.CreateHero();
            heroView.transform.position = model.Hero.InitPosition;
            var hero = new HeroController(model.Hero, heroView);

            GameObject cameraView = viewsFabric.CreateCamera();
            cameraView.transform.position = model.Camera.InitPosition;
            var camera = new CameraController(model.Camera, cameraView);

            var player = new PlayerController(playerInput, cameraView);
            player.Select_Point += hero.AddNewTargetPoint;
            player.Select_Point += camera.AddNewTargetPosition;

            controllers.Add(player);
            controllers.Add(hero);
            controllers.Add(camera);
        }

        private void InitializePirates(
            ControllersStorage controllers, GameModel model)
        {
            PiratesViewFabric viewFabric = new PiratesViewFabric();
            for (int i = 0; i < model.Pirates.Length; ++i)
            {
                InitializeSinglePirate(model.Pirates[i], 
                    viewFabric.CreateGameObject(), controllers);
            }
        }

        private void InitializeSinglePirate(IPersonModel personModel, 
            GameObject view, ControllersStorage controllers)
        {
            var pirate = new PirateController(personModel, view);
            controllers.Add(pirate);
        }
    }
}
