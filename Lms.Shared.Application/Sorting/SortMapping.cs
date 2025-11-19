namespace Lms.Shared.Application.Sorting
{
    public sealed record SortMapping(string SortField, string PropertyName, bool Reverse = false);

}
