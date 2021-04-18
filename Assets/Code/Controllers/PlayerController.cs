using Assets.Code.Exceptions;
using Assets.Code.Interfaces;
using Assets.Code.PlayerInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Controllers
{
    delegate void SelectPoint(Vector3 point);

    internal sealed class PlayerController : IExecute, IInitialization
    {
        public IPlayerInput Input { get; set; }

        public void Initialize()
        {
            if (null == Input)
                throw new GameException(
                    "PlayerController: input is absent. ");
        }

        public void Execute(float deltaTime)
        {
            throw new GameException("");
        }



        public SelectPoint Select_Point;
    }
}
