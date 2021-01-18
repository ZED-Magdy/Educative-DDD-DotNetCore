using Educative.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Persistence.EFCore
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> opt): base(opt)
        {
        }

        public DbSet<Track> Tracks { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<MediaObject> MediaObjects { get; set; }
    }
}
