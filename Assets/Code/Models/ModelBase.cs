using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    internal abstract class ModelBase
    {
        public int ModelId { get; }
        private static int _modelId = 0;

        internal ModelBase()
        {
            ModelId = ++ _modelId;
        }
    }
}
