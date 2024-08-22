using rhinobill.component.tests.Builders;
using rhinobill.component.tests.Data.Fakers;
using rhinobill.component.tests.Extensions;
using rhinobill.component.tests.Setup;

namespace rhinobill.component.tests.Features.Students
{
    public class DeleteStudentTests : BaseTestClass
    {
        public DeleteStudentTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task Delete_student_should_be_success()
        {
            var student = new StudentEntityFaker()
                .Generate();

            await DbContext.AddAsync(student);
            await DbContext.SaveChangesAsync();

            var request = new HttpRequestBuilder($"/api/students/{student.Id}", HttpMethod.Delete)
               .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await response.Content.ReadAsAsync<Guid>();
            result.Should().Be(student.Id);

            var deletedEntity = await DbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == student.Id);
            deletedEntity.Should().BeNull();
        }

        [Fact]
        public async Task Delete_student_should_return_not_found()
        {
            var payload = new CreateStudentRequestFaker()
                .Generate();

            var request = new HttpRequestBuilder($"/api/students/{Guid.NewGuid()}", HttpMethod.Delete)
             .WithPayload(payload)
             .Create();

            var response = await ApiClient.SendAsync(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
