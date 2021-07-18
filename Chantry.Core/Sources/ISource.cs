using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Sources
{
    public interface ISource
    {
        public Bitmap Image { get; }
    }
}
