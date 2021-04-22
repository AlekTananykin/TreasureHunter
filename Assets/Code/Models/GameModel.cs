using Assets.Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    internal sealed class GameModel
    {
        public CameraModel Camera { get; set; }
        public PersonModel Hero { get; set; }

        public PersonModel[] Pirates { get; set; }

        public TreasureChestModel[] Chests { get; set; }
    }
}
