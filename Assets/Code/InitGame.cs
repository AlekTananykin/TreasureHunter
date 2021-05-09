using Assets.Code.Auxiliary;
using Assets.Code.Controllers;
using Assets.Code.Exceptions;
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


            IAttackSystem actionsController = InitActions(controllers);

            HeroController hero = InitializeCameraAndPlayer(viewsFabric,
                controllers, model, playerInput, actionsController);

            InitializeChests(controllers, model);
            InitializePirates(controllers, model, actionsController, hero);

        }

        IAttackSystem InitActions(ControllersStorage controllers)
        {
            IAttackSystem actionSystem = new ActionsController();
            if (!(actionSystem is IInteractionObject))
                throw new GameException(
                    "InitGame.InitActions: ActionsController is not IAttackSystem");

            controllers.Add(actionSystem as IInteractionObject);

            actionSystem.Add(LootName.gun, new ShootoutController<BombViewFabric>());
            actionSystem.Add(LootName.cutlass, new PrickAttackController());

            return actionSystem;
        }

        private HeroController InitializeCameraAndPlayer(
            ViewsFabric viewsFabric,
            ControllersStorage controllers, GameModel model,
            IPlayerInput playerInput,
            IAttackSystem actionSysterm)
        {
            GameObject heroView = viewsFabric.CreateAnastasia();
            heroView.transform.position = model.Hero.InitPosition;
            var hero = new HeroController(model.Hero, heroView, actionSysterm);
            
            GameObject cameraView = viewsFabric.CreateCamera();
            cameraView.transform.position = model.Camera.InitPosition;
            var camera = new CameraController(model.Camera, cameraView);

            GameObject zondView = viewsFabric.CreateZond();
            GameObject frontCameraView = viewsFabric.CreateFrontCamera();
            var zondController = new ZondController(frontCameraView, zondView, heroView);
            
            var player = new PlayerController(playerInput, cameraView, hero);

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
            IAttackSystem actionSystem,
            HeroController hero)
        {
            PiratesViewFabric viewFabric = new PiratesViewFabric();
            for (int i = 0; i < model.Pirates.Length; ++i)
            {
                var pirate = new PirateController(
                model.Pirates[i], viewFabric.CreateGameObject(), actionSystem, hero);
                pirate.SelectAction(0);

                controllers.Add(pirate);
            }
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
