using Assets.Code.Controllers;
using Assets.Code.Models;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
using UnityEngine;

namespace Assets.Code
{
    sealed class InitGame
    {
        public InitGame(ControllersStorage controllers, GameModel model, 
            IPlayerInput playerInput)
        {
            ViewsFabric viewsFabric = new ViewsFabric();

            InitializeCameraAndPlayer(viewsFabric, controllers, model, playerInput);
        }

        private void InitializeCameraAndPlayer(
            ViewsFabric viewsFabric,
            ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput)
        {
            var player = new PlayerController() 
                { Input = playerInput};

            GameObject heroView = viewsFabric.CreateHero();
            var hero = new HeroController() 
                {View = heroView, Model = model.Hero };

            GameObject cameraView = viewsFabric.CreateCamera();
            var camera = new CameraController() { 
                View = cameraView, Model = model.CameraModel};

            player.Select_Point += hero.AddNewTargetPoint;
            player.Select_Point += camera.AddNewTargetPosition;

            controllers.Add(player);
            controllers.Add(hero);
            controllers.Add(camera);
        }
    }
}
