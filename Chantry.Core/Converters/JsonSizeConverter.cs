using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Converters
{
    public class JsonSizeConverter : JsonConverter<SKSizeI>
    {
        public override SKSizeI ReadJson(JsonReader reader, Type objectType, SKSizeI existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] values = reader.Value.ToString().Split(',');

            return new SKSizeI(
                Int32.Parse(values[0]),
                Int32.Parse(values[1]));
        }

        public override void WriteJson(JsonWriter writer, SKSizeI value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Width}, {value.Height}");
        }
    }
}
