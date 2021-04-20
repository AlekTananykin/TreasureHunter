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
        internal void InitGameModel(out GameModel model)
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
