using AccessDesk_ASP_Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessDesk_ASP_Server.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
    }
}
