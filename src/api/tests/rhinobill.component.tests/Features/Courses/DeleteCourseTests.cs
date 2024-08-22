using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Courses
{
    public class DeleteCourseTests : BaseTestClass
    {
        public DeleteCourseTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Delete_course_should_be_success()
        {
            var entity = new CourseEntityFaker()
                .Generate();

            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            var request = new HttpRequestBuilder($"/api/courses/{entity.Id}", HttpMethod.Delete)
               .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Guid>();
            result.Should().Be(entity.Id);

            var deletedStudent = await DbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
            deletedStudent.Should().BeNull();
        }

        [Fact]
        public async Task Delete_course_should_return_not_found()
        {
            var payload = new CreateStudentRequestFaker()
                .Generate();

            var request = new HttpRequestBuilder($"/api/courses/{Guid.NewGuid()}", HttpMethod.Delete)
             .WithPayload(payload)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
