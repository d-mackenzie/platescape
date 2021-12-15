using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Converters
{
    public class SourceConverter : JsonConverter<ISource>
    {
        public override ISource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ISource value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is FileSource fileSource)
            {
                writer.WriteString("Filename", fileSource.Filename);
            }

            writer.WriteEndObject();
        }
    }
}
