using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;
using TeethInc.Chantry.Core.Extensions;

namespace TeethInc.Chantry.Core.Services
{
    public static class ProjectService
    {
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string SerializeProject(Project project)
        {
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(project, jsonSerializerOptions);
        }

        public static Project DeserializeProject(string json)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(json);

            ISource source =  DeserializeSource(jsonDocument.RootElement.GetProperty("source"));

            return new Project()
            {
                Source = source
            };
        }

        private static ISource DeserializeSource(JsonElement jsonElement)
        {
            ISource ret = null;

            switch (jsonElement.GetProperty("type").GetString())
            {
                case "filesource":
                    ret = Deserialize<FileSource>(jsonElement.GetProperty("object"));
                    break;

                default:
                    break;
            }

            return ret;
        }

        private static T Deserialize<T>(JsonElement jsonElement)
        {
            return JsonSerializer.Deserialize<T>(
                jsonElement.GetRawText(),
                jsonSerializerOptions);
        }
    }
}
