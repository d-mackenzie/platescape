using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Core.Ldraw
{
    public class LdFile
    {
        private List<LdPartLine> m_parts = new List<LdPartLine>();

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
            m_parts.Add(new LdPartLine(ldPart, ldColor, x, y, z));
        }

        public IEnumerable<string> ToList()
        {
            var ret = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                ret.Add($"0 Name: {Name}");
            }
            
            ret.Add("0 Author: Chantry");

            foreach (LdPartLine ldPartLine in m_parts)
            {
                ret.Add($"1 {ldPartLine.LdColor.Number} {ldPartLine.X} {ldPartLine.Y} {ldPartLine.Z} 1 0 0 0 1 0 0 0 1 {ldPartLine.LdPart.Number}.dat");
            }

            return ret;
        }

        private class LdPartLine
        {
            public LdPart LdPart { get; }

            public LdColor LdColor { get; }

            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public LdPartLine(LdPart ldPart, LdColor ldColor, int x, int y, int z)
            {
                LdPart = ldPart;
                LdColor = ldColor;
                X = x;
                Y = y;
                Z = z;
            }
        }

    }
}
