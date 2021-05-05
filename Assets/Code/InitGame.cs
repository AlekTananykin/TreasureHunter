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
            var prickAttackController = new PrickAttackController();

            HeroController hero = InitializeCameraAndPlayer(viewsFabric,
                controllers, model, playerInput, shootOutController);

            InitializeChests(controllers, model);
            InitializePirates(controllers, model, shootOutController, prickAttackController, hero);

            controllers.Add(shootOutController);
        }

        private HeroController InitializeCameraAndPlayer(
            ViewsFabric viewsFabric,
            ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput,
            IAttackSystem attackSystem)
        {
            GameObject heroView = viewsFabric.CreateHero();
            heroView.transform.position = model.Hero.InitPosition;
            var hero = new HeroController(model.Hero, heroView, attackSystem);
            
            GameObject cameraView = viewsFabric.CreateCamera();
            cameraView.transform.position = model.Camera.InitPosition;
            var camera = new CameraController(model.Camera, cameraView);

            GameObject zondView = viewsFabric.CreateZond();
            GameObject frontCameraView = viewsFabric.CreateFrontCamera();
            var zondController = new ZondController(frontCameraView, zondView, heroView);
            
            var player = new PlayerController(playerInput, cameraView);

            player.Hit_To_Point += hero.HitToPoint;
            player.Go_To_Point += hero.AddNewTargetPoint;
            player.Take_Loot += hero.TakeLoot;

            player.Go_To_Point += camera.AddNewTargetPosition;
            player.Current_Point += zondController.SetPosition;
            
            controllers.Add(player);
            controllers.Add(hero);
            controllers.Add(camera);
            controllers.Add(zondController);

            return hero;
        }

        private void InitializePirates(
            ControllersStorage controllers, GameModel model,
            IAttackSystem shootAttack,
            IAttackSystem prickAttack,
            HeroController hero)
        {
            PiratesViewFabric viewFabric = new PiratesViewFabric();
            for (int i = 0; i < model.Pirates.Length; ++i)
            {
                InitializeSinglePirate(model.Pirates[i], 
                    viewFabric.CreateGameObject(), 
                    ((i % 2) == 0)? shootAttack: prickAttack, 
                    controllers, hero);
            }
        }

        private void InitializeSinglePirate(IPersonModel personModel, 
            GameObject view, IAttackSystem bombShootout, 
            ControllersStorage controllers, HeroController hero)
        {
            var pirate = new PirateController(
                personModel, view, bombShootout, hero);

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
