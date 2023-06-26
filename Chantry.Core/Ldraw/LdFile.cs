using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Ldraw
{
    public class LdFile
    {
        private List<LdPartLine> _parts = new List<LdPartLine>();

        public string Name { get; set; }

        public string Author { get; set; }

        public LdFile() { }

        public LdFile(string name, string author)
        {
            Name = name;
            Author = author;
        }

        public void Add(LdPart ldPart, LdColor ldColor, int x, int y, int z)
        {
            _parts.Add(new LdPartLine(ldPart, ldColor, x, y, z));
        }

        public IEnumerable<string> AsEnumerable()
        {
            List<string> headers = new List<string>
            {
                $"0 Name: {Name}",
                "0 Author: Chantry"
            };

            return headers.Concat(_parts.Select(x => x.ToString()));
        }

        private class LdPartLine
        {
            public LdPart LdPart { get; }

            public LdColor LdColor { get; }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Z { get; private set; }

            public LdPartLine(LdPart ldPart, LdColor ldColor, int x, int y, int z)
            {
                LdPart = ldPart;
                LdColor = ldColor;
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString()
            {
                return $"1 {LdColor.Number} {X} {Y} {Z} 1 0 0 0 1 0 0 0 1 {LdPart.Number}.dat";
            }
        }
    }
}
