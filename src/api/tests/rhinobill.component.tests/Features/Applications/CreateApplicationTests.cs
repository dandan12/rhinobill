using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Applications.Models;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Applications
{
    public class CreateApplicationTests : BaseTestClass
    {
        public CreateApplicationTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Create_application_should_be_successful()
        {
            var student = new StudentEntityFaker()
                .Generate();
            var course = new CourseEntityFaker()
                .Generate();

            await DbContext.AddRangeAsync(student, course);
            await DbContext.SaveChangesAsync();

            var payload = new CreateApplicationRequestFaker(student.Id, course.Id).Generate();

            var request = new HttpRequestBuilder("/api/applications", HttpMethod.Post)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Application>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var newEntity = await DbContext.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == result.Id);
            newEntity.Should().NotBeNull();
            newEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Create_application_should_return_bad_request()
        {
            var payload = new CreateApplicationRequestFaker(Guid.Empty, Guid.Empty).Generate();

            var request = new HttpRequestBuilder("/api/applications", HttpMethod.Post)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_application_should_return_not_found()
        {
            var payload = new CreateApplicationRequestFaker(Guid.NewGuid(), Guid.NewGuid()).Generate();

            var request = new HttpRequestBuilder("/api/applications", HttpMethod.Post)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
