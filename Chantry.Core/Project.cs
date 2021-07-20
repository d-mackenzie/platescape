using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core
{
    public class Project
    {
        public ISource Source { get; set; }
    }
}
