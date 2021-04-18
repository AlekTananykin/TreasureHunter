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
        public ICameraModel CameraModel { get; set; }
        public IHeroModel Hero { get; set; }


    }
}
