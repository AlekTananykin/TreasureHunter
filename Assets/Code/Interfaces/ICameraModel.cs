using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    interface ICameraModel
    {
        float Speed { get; set; }
        Vector3 InitPosition { get; set; }
    }
}
