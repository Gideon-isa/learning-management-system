namespace Lms.Identity.Application.Abstractions
{
    internal interface IOutboxService
    {
        Task<bool> AddInstructorRequestEvent();
    }
}
