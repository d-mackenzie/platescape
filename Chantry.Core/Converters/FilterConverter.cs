using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Converters
{
    public class FilterConverter : JsonConverter<Filter>
    {
        public override Filter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Filter value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Type", value.Type);

            switch (value)
            {
                case BrightnessContrastFilter bcf:
//                    writer.WriteObject(bcf);
                    break;

                default:
                    break;
            }

            writer.WriteEndObject();
        }
    }
}
