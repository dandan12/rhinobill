using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Converters
{
    public static class JsonSerializeSetting
    {
        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> 
                { 
                    new DateOnlyJsonConverter(),
                    new EmailJsonConverter()
                }
            };
        }
    }
}
