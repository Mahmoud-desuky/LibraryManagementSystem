using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class ApplicationDbContext:DbContext
    {
        //Data Source=.;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=LibararySeystem;Integrated Security=True;Trust Server Certificate=true");
        }
     /*   protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Library>().HasMany<Book>.withone()
        }
     */
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Library> Librarys { get; set; }
       public DbSet<Admain_user> Admain_Users { get; set; }

    }
}
