using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry
{
    public class Configuration
    {
        public IEnumerable<string> Mru { get; set; }

        public Configuration()
        {
            Mru = new List<string>();
        }
    }
}
