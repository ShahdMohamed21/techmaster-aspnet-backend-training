using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Entities.Instructors;
using TrainingCenter.Api.Entities.Students;
using TrainingCenter.Api.Entities.Tracks;

namespace TrainingCenter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<TrainingTrack> TrainingTracks { get; set; }

    }
}
