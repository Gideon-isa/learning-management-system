using Lms.SharedKernel.Domain;

namespace Lms.CourseDelivery.Domain.Entities
{
    public class CourseDeliveryModule : Entity<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

    }
}
