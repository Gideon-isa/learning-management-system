namespace Lms.Api.Contracts.Courses
{
    public class GetCoursesRequest
    {
        public string? Search { get; set; }
        public string? CourseTitle { get; set; }
        public string? CourseCode { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Sort { get; init; }
        public string? Fields { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
