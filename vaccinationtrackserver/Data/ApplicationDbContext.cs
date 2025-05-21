// Namespace: Groups related classes under the project
// Must match the project name (vaccinationtrackserver)
namespace vaccinationtrackserver.Data
{
    // Using statements: Import necessary namespaces for EF Core and models
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Models;

    // ApplicationDbContext: Inherits from DbContext to interact with the database
    // Not abstract or generic, as required by EF Core migrations
    public class ApplicationDbContext : DbContext
    {
        // Constructor: Accepts DbContextOptions to configure the context
        // DbContextOptions: Contains settings like the connection string
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet<User>: Represents the Users table for CRUD operations
        public DbSet<User> Users { get; set; }

        // DbSet<Student>: Represents the Students table for CRUD operations
        public DbSet<Student> Students { get; set; }

        // DbSet<VaccinationDrive>: Represents the VaccinationDrives table
        public DbSet<VaccinationDrive> VaccinationDrives { get; set; }

        // DbSet<VaccinationRecord>: Represents the VaccinationRecords table
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

        // OnModelCreating: Configures the database schema and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraint for Users.Username
            // HasIndex: Creates an index on Username
            // IsUnique: Ensures no duplicate usernames
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure check constraint for VaccinationDrive.DriveDate
            // HasCheckConstraint: Ensures drives are scheduled 15 days in advance
            // Note: EF Core with Npgsql doesn't fully support check constraints, so this is for documentation
            modelBuilder.Entity<VaccinationDrive>()
                .HasCheckConstraint("CK_DriveDate", "DriveDate >= CURRENT_DATE + INTERVAL '15 days'");

            // Configure unique constraint for VaccinationDrive.DriveDate
            // HasIndex: Creates an index on DriveDate
            // IsUnique: Prevents overlapping drives
            modelBuilder.Entity<VaccinationDrive>()
                .HasIndex(d => d.DriveDate)
                .IsUnique();

            // Configure unique constraint for VaccinationRecord
            // HasIndex: Creates a composite index on StudentID and VaccineName
            // IsUnique: Prevents duplicate vaccinations for the same vaccine
            modelBuilder.Entity<VaccinationRecord>()
                .HasIndex(r => new { r.StudentID, r.VaccineName })
                .IsUnique();
        }
    }
}