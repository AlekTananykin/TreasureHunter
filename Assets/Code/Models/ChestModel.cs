﻿using Assets.Code.Auxiliary;
using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    sealed  internal class ChestModel
    {
        public IList<IThing> Items { get; set; }
        
        public Vector3 Position { get; set; }
    }
}
