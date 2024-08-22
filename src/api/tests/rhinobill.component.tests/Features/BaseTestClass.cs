using Microsoft.AspNetCore.TestHost;
using rhinobill.component.tests.Setup;
using rhinobill.sql;

namespace rhinobill.component.tests.Features
{
    public class BaseTestClass : IClassFixture<TestFixture>
    {
        public BaseTestClass(TestFixture testFixture)
        {
            ApiClient = testFixture.CreateClient();
            Server = testFixture.Server;
            DbContext = testFixture.DbContext;
        }

        public HttpClient ApiClient { get; }
        public TestServer Server { get; }
        public AppDbContext DbContext { get; }
    }
}
