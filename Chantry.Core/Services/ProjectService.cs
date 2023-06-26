using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Core.Extensions;
using TeethInc.Chantry.Core.Converters;
using Newtonsoft.Json;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
    public static class ProjectService
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static string SerializeProject(Project project)
        {
            return JsonConvert.SerializeObject(project, Formatting.Indented, _jsonSerializerSettings);
        }

        public static Project DeserializeProject(string json)
        {
            return JsonConvert.DeserializeObject<Project>(json, _jsonSerializerSettings);
        }
    }
}
