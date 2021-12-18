using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Converters
{
    public class JsonSizeConverter : JsonConverter<Size>
    {
        public override Size ReadJson(JsonReader reader, Type objectType, Size existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] values = reader.Value.ToString().Split(',');

            return new Size(
                Int32.Parse(values[0]),
                Int32.Parse(values[1]));
        }

        public override void WriteJson(JsonWriter writer, Size value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Width}, {value.Height}");
        }
    }
}
