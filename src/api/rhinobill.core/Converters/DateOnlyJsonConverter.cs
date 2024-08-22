using Newtonsoft.Json;
using rhinobill.core.Extensions;
using System.Globalization;

namespace rhinobill.core.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy/MM/dd";

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (DateTime.TryParse(reader?.Value?.ToString(), out var datetime))
            {
                return datetime.ToDateOnly();
            }

            return default;
        }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
