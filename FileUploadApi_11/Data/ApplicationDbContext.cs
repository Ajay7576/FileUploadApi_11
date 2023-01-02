using FileUploadApi_11.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploadApi_11.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<BatchUploadDetail> BatchUploadDetails { get; set; }
        public DbSet<Employee> employees { get; set; }

        public DbSet<Patient> Patients { get; set; }


    }
}
