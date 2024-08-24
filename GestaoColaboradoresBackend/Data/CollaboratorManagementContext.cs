using GestaoColaboradoresBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradoresBackend.Data
{
    public class CollaboratorManagementContext : DbContext
    {
        public CollaboratorManagementContext(DbContextOptions<CollaboratorManagementContext> options) : base(options) { }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collaborator>()
                .Property(c => c.Salary)
                .HasColumnType("decimal(18,2)");
        }
    }
}
