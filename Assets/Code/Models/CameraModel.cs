using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    class CameraModel : ICameraModel
    {
        public float Speed { get; set; }
        public Vector3 InitPosition { get; set; }
    }
}
