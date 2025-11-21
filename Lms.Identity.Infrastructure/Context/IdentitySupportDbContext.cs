using Lms.Identity.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Lms.Identity.Infrastructure.Context
{
    public class IdentitySupportDbContext : DbContext
    {
        public IdentitySupportDbContext(DbContextOptions<IdentitySupportDbContext> options) : base(options) { }
        public DbSet<IdentityOutboxMessage> IdentityOutboxMessages => Set<IdentityOutboxMessage>();
    }
}
