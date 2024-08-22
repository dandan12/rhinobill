using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Applications.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;
using rhinobill.sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Applications
{
    public class UpdateApplicationTests : BaseTestClass
    {
        public UpdateApplicationTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Update_application_should_be_success()
        {
            var student = new StudentEntityFaker()
              .Generate();
            var course = new CourseEntityFaker()
                .Generate();

            var entity = new ApplicationEntity
            {
                Id = Guid.NewGuid(),
                ApplicationDate = DateTime.UtcNow,
                CourseId = course.Id,
                StudentId = student.Id
            };

            await DbContext.AddRangeAsync(student, course, entity);
            await DbContext.SaveChangesAsync();

            var payload = new CreateApplicationRequestFaker(student.Id, course.Id).Generate();

            var request = new HttpRequestBuilder($"/api/applications/{entity.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Application>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var updatedEntity = await DbContext.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
            updatedEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Update_application_should_return_bad_request()
        {
            var student = new StudentEntityFaker()
                .Generate();
            var course = new CourseEntityFaker()
                .Generate();

            var entity = new ApplicationEntity
            {
                Id = Guid.NewGuid(),
                ApplicationDate = DateTime.UtcNow,
                CourseId = course.Id,
                StudentId = student.Id
            };

            await DbContext.AddRangeAsync(student, course, entity);
            await DbContext.SaveChangesAsync();

            var payload = new CreateApplicationRequestFaker(Guid.Empty, Guid.Empty).Generate();

            var request = new HttpRequestBuilder($"/api/applications/{entity.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_application_should_return_not_found()
        {
            var student = new StudentEntityFaker()
               .Generate();
            var course = new CourseEntityFaker()
                .Generate();

            var entity = new ApplicationEntity
            {
                Id = Guid.NewGuid(),
                ApplicationDate = DateTime.UtcNow,
                CourseId = course.Id,
                StudentId = student.Id
            };

            await DbContext.AddRangeAsync(student, course, entity);
            await DbContext.SaveChangesAsync();

            var payload = new CreateApplicationRequestFaker(student.Id, course.Id).Generate();

            var request = new HttpRequestBuilder($"/api/applications/{Guid.NewGuid()}", HttpMethod.Put)
             .WithPayload(payload)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
