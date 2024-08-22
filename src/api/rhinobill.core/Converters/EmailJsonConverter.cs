using Newtonsoft.Json;
using rhinobill.core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Converters
{
    public class EmailJsonConverter : JsonConverter<Email>
    {
        public override Email? ReadJson(JsonReader reader, Type objectType, Email? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Email.ParseOrDefault(reader.Value?.ToString());
        }

        public override void WriteJson(JsonWriter writer, Email? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }
}
