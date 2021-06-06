using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Ldraw
{
    public class LdPart
    {
        public int Number { get; }
        public string Name { get; }

        public Size Size { get; }

        public LdPart(int number, string name, Size size)
        {
            Number = number;
            Name = name;
            Size = size;
        }
    }
}
