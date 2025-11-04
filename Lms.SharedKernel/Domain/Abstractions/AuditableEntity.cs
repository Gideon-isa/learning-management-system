namespace Lms.SharedKernel.Domain.Abstractions
{
    internal abstract class AuditableEntity
    {
        public DateTime CreatedOn { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public DateTime? LastModifiedOn { get; protected set; }
        public string? LastModifiedBy { get; protected set; }
    }
}
