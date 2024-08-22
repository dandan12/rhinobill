using rhinobill.core.Models.Common;

namespace rhinobill.api.Contracts.Requests.Students
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Email Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
