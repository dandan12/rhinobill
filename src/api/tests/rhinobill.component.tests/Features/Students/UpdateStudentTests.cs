using Microsoft.EntityFrameworkCore;
using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Extensions;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;

namespace rhinobill.component.tests.Features.Students
{
    public class UpdateStudentTests : BaseTestClass
    {
        public UpdateStudentTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Update_student_should_be_success()
        {
            var student = new StudentEntityFaker()
                .Generate();

            await DbContext.AddAsync(student);
            await DbContext.SaveChangesAsync();

            var payload = new CreateStudentRequestFaker().Generate();

            var request = new HttpRequestBuilder($"/api/students/{student.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Student>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var updatedEntity = await DbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == student.Id);
            updatedEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Update_student_should_return_bad_request()
        {
            var student = new StudentEntityFaker()
                .Generate();

            await DbContext.AddAsync(student);
            await DbContext.SaveChangesAsync();

            var payload = new CreateStudentRequestFaker()
                .RuleFor(x => x.FirstName, "a")
                .Generate();

            var request = new HttpRequestBuilder($"/api/students/{student.Id}", HttpMethod.Put)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var result = await response.Content.ReadAsAsync<ErrorResult>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                Code = Errors.MinMaxLengthCode,
                Message = Errors.MinMaxLength230Message,
                Type = ErrorType.BadRequest
            }, opt => opt.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Update_student_should_return_not_found()
        {
            var payload = new CreateStudentRequestFaker()
                .Generate();

            var request = new HttpRequestBuilder($"/api/students/{Guid.NewGuid()}", HttpMethod.Put)
             .WithPayload(payload)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
