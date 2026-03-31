using SkiaSharp;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TeethInc.Chantry.Core.Converters
{
    public class SizeJsonConverter : JsonConverter<SKSizeI>
    {
        public override SKSizeI Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            string[] values = reader.GetString()?.Split(',');

            return new SKSizeI(
                Int32.Parse(values[0]),
                Int32.Parse(values[1]));
        }

        public override void Write(Utf8JsonWriter writer, SKSizeI value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.Width}, {value.Height}");
        }
    }
}
