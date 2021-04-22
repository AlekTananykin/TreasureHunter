using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    internal sealed class GameModelFabric
    {
        System.Random _rand = new System.Random();
        float _minDistance = 10f;
        int _piratesRange = 1000;

        internal GameModel InitGameModel(uint piratesNum)
        {
            GameModel model = new GameModel();
            model.Hero = CreateHeroModel();
            model.Camera = CreateCameraModel(model.Hero);

            model.Pirates = new PersonModel[piratesNum];
            for (int i = 0; i < piratesNum; ++i)
            {
                model.Pirates[i] = CreatePirateModel(
                    GetRandPiratePos(model.Hero.InitPosition, 
                    model.Pirates, i, _minDistance));
            }

            return model;
        }

        private Vector3 GetRandPiratePos(Vector3 heroInitPosition,
            PersonModel[] piratesPos, int count, float minDistance)
        {
            const int matTryCount = 15;
            Vector3 pos = new Vector3();
            for(int i = 0; i < matTryCount; ++ i)
            {
                pos = new Vector3( _rand.Next(0, _piratesRange), 
                    heroInitPosition.y, _rand.Next(0, _piratesRange));

                if ((heroInitPosition - pos).magnitude < minDistance)
                    continue;

                if (CheckPirates(pos, piratesPos, count, minDistance))
                    break;
            } 

            return pos;
        }

        private bool CheckPirates(Vector3 pos, PersonModel[] piratesPos, 
            int count, float minDistance)
        {
            for (int j = 0; j < count; ++j)
            {
                if ((piratesPos[j].InitPosition - pos).magnitude < minDistance)
                    return false;
            }
            return true;
        }

        private PersonModel CreateHeroModel()
        {
            var model = new PersonModel();
            model.Health = 100;
            model.MaxHealth = 100;
            model.InitPosition = new Vector3(10, 3, 10);
            model.Skill = 20;
            model.Speed = 5;
            return model;
        }

        private CameraModel CreateCameraModel(PersonModel heroModel)
        {
            var model = new CameraModel();
            model.Forward = Vector3.down;
            model.Height = 30f;
            model.InitPosition = new Vector3(
                model.InitPosition.x,
                model.Height, heroModel.InitPosition.z);
            model.Speed = 10f;
            return model;
        }

        private PersonModel CreatePirateModel(Vector3 initPosition)
        {
            var model = new PersonModel();
            model.Health = 100;
            model.MaxHealth = 100;
            model.InitPosition = initPosition;
            model.Skill = 19;
            model.Speed = 7f;
            return model;
        }

    }
}
