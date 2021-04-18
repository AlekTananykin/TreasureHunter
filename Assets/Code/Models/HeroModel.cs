using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    class HeroModel
    {
        public Vector3 Position { get; set; }
        public float Speed { get; set; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        
        public int Skill { get; set; }
    }
}
