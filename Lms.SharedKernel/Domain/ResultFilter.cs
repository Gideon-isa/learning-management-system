namespace Lms.SharedKernel.Domain
{
    public class ResultFilter
    {
        public string? Search { get; set; }
        public string? Sort { get; init; }
        public string? Fields { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
