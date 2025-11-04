using Lms.Identity.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Lms.Identity.Infrastructure.Context
{
    public class UserIdentityDbContext : BaseDbContext
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) :base(options) { }

        public DbSet<IdentityOutboxMessage> IdentityOutboxMessages => Set<IdentityOutboxMessage>();
    }
}
