using Microsoft.EntityFrameworkCore;

namespace Lms.CourseDelivery.Infrastructure.DataContext
{
    public class CourseDeliveryDbContext : DbContext
    {
        public CourseDeliveryDbContext(DbContextOptions<CourseDeliveryDbContext> options) : base (options)
        {
            
        }
    }
}
