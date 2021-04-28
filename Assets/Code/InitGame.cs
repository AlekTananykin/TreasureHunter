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

            var shootOutController = new ShootoutController<BombViewFabric>();

            HeroController hero = InitializeCameraAndPlayer(viewsFabric,
                controllers, model, playerInput, shootOutController);

            InitializeChests(controllers, model);
            InitializePirates(controllers, model, shootOutController, hero);

            controllers.Add(shootOutController);
        }

        private HeroController InitializeCameraAndPlayer(
            ViewsFabric viewsFabric,
            ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput,
            ShootoutController<BombViewFabric> shootOutSystem)
        {
            GameObject heroView = viewsFabric.CreateHero();
            heroView.transform.position = model.Hero.InitPosition;
            var hero = new HeroController(model.Hero, heroView);
            hero.Shoot += shootOutSystem.Shoot;

            GameObject cameraView = viewsFabric.CreateCamera();
            cameraView.transform.position = model.Camera.InitPosition;
            var camera = new CameraController(model.Camera, cameraView);

            GameObject zondView = viewsFabric.CreateZond();
            GameObject frontCameraView = viewsFabric.CreateZond();
            var zondController = new ZondController(zondView, frontCameraView, heroView);

            var player = new PlayerController(playerInput, cameraView);

            player.Hit_To_Point += hero.HitToPoint;
            player.Go_To_Point += hero.AddNewTargetPoint;
            player.Go_To_Point += camera.AddNewTargetPosition;
            player.Current_Point += zondController.SetPosition;


            controllers.Add(player);
            controllers.Add(hero);
            controllers.Add(camera);

            return hero;
        }

        private void InitializePirates(
            ControllersStorage controllers, GameModel model,
            ShootoutController<BombViewFabric> bombShootout,
            HeroController hero)
        {
            PiratesViewFabric viewFabric = new PiratesViewFabric();
            for (int i = 0; i < model.Pirates.Length; ++i)
            {
                InitializeSinglePirate(model.Pirates[i], 
                    viewFabric.CreateGameObject(), bombShootout, 
                    controllers, hero);
            }
        }

        private void InitializeSinglePirate(IPersonModel personModel, 
            GameObject view, ShootoutController<BombViewFabric> bombShootout, 
            ControllersStorage controllers, HeroController hero)
        {
            var pirate = new PirateController(personModel, view, hero);

            pirate.Shoot += bombShootout.Shoot;
            controllers.Add(pirate);
        }

        private void InitializeChests(
            ControllersStorage controllers, GameModel model)
        {
            ChestViewFabric viewFabric = new ChestViewFabric();
            for (int i = 0; i < model.Chests.Length; ++i)
            {
                InitializeSingleChest(model.Chests[i],
                    viewFabric.CreateGameObject(), controllers);
            }
        }

        private void InitializeSingleChest(ChestModel chestModel, 
            GameObject view, ControllersStorage controllers)
        {
            var chest = new ChestController(chestModel, view);
            controllers.Add(chest);
        }
    }
}
