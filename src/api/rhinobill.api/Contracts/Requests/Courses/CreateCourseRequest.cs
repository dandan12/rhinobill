namespace rhinobill.api.Contracts.Requests.Courses
{
    public class CreateCourseRequest
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
