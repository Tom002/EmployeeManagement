using EmployeeManagement.Common.RequestContext;
using EmployeeManagement.Dal.Entities;
using EmployeeManagement.Dal.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeManagement.Dal
{
    public partial class EmployeeManagementDbContext : DbContext
    {
        private readonly IRequestContext _requestContext;

        public EmployeeManagementDbContext(
            DbContextOptions<EmployeeManagementDbContext> options,
            IRequestContext requestContext)
            : base(options)
        {
            _requestContext = requestContext;
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterSoftDeleteQueryFilters(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(x => x.Name).HasMaxLength(100);
                entity.Property(x => x.Active).HasDefaultValue(true);
                entity.Property(x => x.LastModById).HasMaxLength(100);
                entity.Property(x => x.CreatedById).HasMaxLength(100);

                entity.HasOne(x => x.Boss)
                    .WithMany(x => x.Subordinates)
                    .HasForeignKey(x => x.BossId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Department)
                    .WithMany(x => x.Employees)
                    .HasForeignKey(x => x.DepartmentId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.Position).HasMaxLength(100);
                entity.Property(x => x.Phone).HasMaxLength(16);

                entity.Property(x => x.Username).HasMaxLength(100);
                entity.HasIndex(b => b.Username).IsUnique();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(x => x.Name).HasMaxLength(100);
                entity.Property(x => x.Active).HasDefaultValue(true);
                entity.Property(x => x.LastModById).HasMaxLength(100);
                entity.Property(x => x.CreatedById).HasMaxLength(100);

                entity.Property(x => x.Abbreviation).HasMaxLength(5);
            });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SaveChangesCore();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SaveChangesCore();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void SaveChangesCore()
        {
            var currentTime = DateTime.Now;

            var deletedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            foreach (var entry in deletedEntries)
            {
                ((IEntity)entry.Entity).Active = false;
                entry.State = EntityState.Modified;
            }

            var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entity in addedEntries.Select(e => e.Entity))
            {
                ((IEntity)entity).CreatedAt = currentTime;
                ((IEntity)entity).LastModAt = currentTime;
                // Nem bejelentkezett user esetén konstans id (100) eltárolása
                ((IEntity)entity).CreatedById = _requestContext.CurrentUserId.HasValue ? _requestContext.CurrentUserId.Value : 100;
                ((IEntity)entity).LastModById = _requestContext.CurrentUserId.HasValue ? _requestContext.CurrentUserId.Value : 100;
            }

            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifiedEntries.Select(e => e.Entity))
            {
                ((IEntity)entity).LastModAt = currentTime;
                ((IEntity)entity).LastModById = _requestContext.CurrentUserId.HasValue ? _requestContext.CurrentUserId.Value : 100;
            }
        }

        private void RegisterSoftDeleteQueryFilters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(x => x.Active == true);
            modelBuilder.Entity<Department>().HasQueryFilter(x => x.Active == true);
        }
    }
}
