using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeLoggingApp.Models;

namespace TimeLoggingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
