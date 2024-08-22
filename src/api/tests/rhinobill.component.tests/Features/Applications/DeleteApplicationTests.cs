using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Setup;
using rhinobill.sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Applications
{
    public class DeleteApplicationTests : BaseTestClass
    {
        public DeleteApplicationTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Delete_application_should_be_success()
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

            var request = new HttpRequestBuilder($"/api/applications/{entity.Id}", HttpMethod.Delete)
               .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Guid>();
            result.Should().Be(entity.Id);

            var deletedStudent = await DbContext.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
            deletedStudent.Should().BeNull();
        }

        [Fact]
        public async Task Delete_course_should_return_not_found()
        {
            var request = new HttpRequestBuilder($"/api/courses/{Guid.NewGuid()}", HttpMethod.Delete)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
