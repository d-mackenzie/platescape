using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Converters;
using Newtonsoft.Json;

namespace TeethInc.Chantry.Core.Services
{
    public static class ProjectService
    {
        public static string SerializeProject(Project project)
        {
            return JsonConvert.SerializeObject(project, Formatting.Indented);
        }











        public static Project DeserializeProject(string json)
        {
            return null;
        }
    }
}
