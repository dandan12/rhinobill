using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Courses
{
    public class UpdateCourseTests : BaseTestClass
    {
        public UpdateCourseTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Update_course_should_be_success()
        {
            var entity = new CourseEntityFaker()
                .Generate();

            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            var payload = new CreateCourseRequestFaker().Generate();

            var request = new HttpRequestBuilder($"/api/courses/{entity.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Course>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var updatedEntity = await DbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
            updatedEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Update_course_should_return_bad_request()
        {
            var entity = new CourseEntityFaker()
                .Generate();

            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            var payload = new CreateCourseRequestFaker()
                .RuleFor(x => x.Code, "a")
                .Generate();

            var request = new HttpRequestBuilder($"/api/courses/{entity.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var result = await response.Content.ReadAsAsync<ErrorResult>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                Code = Errors.MinMaxLengthCode,
                Message = Errors.MinMaxLength510Message,
                Type = ErrorType.BadRequest
            }, opt => opt.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Update_course_should_return_not_found()
        {
            var payload = new CreateCourseRequestFaker()
                .Generate();

            var request = new HttpRequestBuilder($"/api/courses/{Guid.NewGuid()}", HttpMethod.Put)
             .WithPayload(payload)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
