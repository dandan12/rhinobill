using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using rhinobill.sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace rhinobill.component.tests.Setup
{
    public class TestFixture : WebApplicationFactory<Program>, IDisposable
    {
        public TestFixture()
        {
            var scope = Server.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetService<AppDbContext>();
        }

        public AppDbContext DbContext { get; }
    }
}
