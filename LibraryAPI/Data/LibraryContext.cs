using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryApi.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
