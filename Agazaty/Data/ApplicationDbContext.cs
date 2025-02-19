using Agazaty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.ComponentModel;

namespace Agazaty.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CasualLeave> CasualLeaves { get; set; }
        public DbSet<PermitLeave> PermitLeaves { get; set; }
        public DbSet<PermitLeaveImage> PermitLeaveImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
