using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    class CameraModel
    {
        public float Speed { get; set; }
        public Vector3 InitPosition { get; set; }

        public Vector3 Forward { get; set; }
        
        public float Height { get; set; }
    }
}
