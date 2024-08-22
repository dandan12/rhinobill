using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Extensions;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Courses
{
    public class CreateCourseTests : BaseTestClass
    {
        public CreateCourseTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Create_course_should_be_successful()
        {
            // Arrange
            var payload = new CreateCourseRequestFaker().Generate();

            var request = new HttpRequestBuilder("/api/courses", HttpMethod.Post)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Course>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var newEntity = await DbContext.Courses
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == result.Id);
            newEntity.Should().NotBeNull();
            newEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Create_course_should_return_bad_request()
        {
            // Arrange
            var payload = new CreateCourseRequestFaker()
                .RuleFor(x => x.Code, "a")
                .Generate();

            var request = new HttpRequestBuilder("/api/courses", HttpMethod.Post)
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
    }
}
