using Microsoft.EntityFrameworkCore;

namespace iSnippets.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Snippet> SnippetTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=iSnippets.db");
        }
    } 
}
