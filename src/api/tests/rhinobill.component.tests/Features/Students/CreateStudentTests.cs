using FluentAssertions;
using rhinobill.api.Contracts.Requests.Students;
using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Extensions;
using rhinobill.component.tests.Setup;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Constants;
using rhinobill.core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Features.Students
{
    public class CreateStudentTests : BaseTestClass
    {
        public CreateStudentTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Create_student_should_be_successful()
        {
            // Arrange
            var payload = new CreateStudentRequestFaker().Generate();

            var request = new HttpRequestBuilder("/api/students", HttpMethod.Post)
                .WithPayload(payload)
                .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Student>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());

            var newEntity = await DbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == result.Id);
            newEntity.Should().NotBeNull();
            newEntity.Should().BeEquivalentTo(payload, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task Create_student_should_return_bad_request()
        {
            // Arrange
            var payload = new CreateStudentRequestFaker()
                .RuleFor(x => x.FirstName, "a")
                .Generate();

            var request = new HttpRequestBuilder("/api/students", HttpMethod.Post)
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
        
    }
}
