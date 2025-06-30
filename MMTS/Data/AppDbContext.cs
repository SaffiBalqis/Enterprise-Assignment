using Microsoft.EntityFrameworkCore;
using MMTS.Models; // Import your models namespace

namespace MMTS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }

    }
}
