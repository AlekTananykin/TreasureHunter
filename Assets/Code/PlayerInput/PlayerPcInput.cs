using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.PlayerInput
{
    sealed class PlayerPcInput : IPlayerInput
    {
        public float MoveX => Input.GetAxis("Mouse X");
        public float MoveY => Input.GetAxis("Mouse Y");
        public bool IsSelected => Input.GetMouseButtonDown(0);
    }
}
