using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Builders
{
    public class HttpRequestBuilder
    {
        private readonly string path;
        private readonly HttpMethod method;
        private object payload = null;
        private string contentType = "application/json";

        public HttpRequestBuilder(string path, HttpMethod method)
        {
            this.path = path;
            this.method = method;
        }

        public HttpRequestBuilder WithPayload(object payload)
        {
            this.payload = payload;
            return this;
        }

        public HttpRequestMessage Create()
        {
            var request = new HttpRequestMessage(method, path);
            if (payload != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, contentType);
            }

            return request;
        }
    }
}
