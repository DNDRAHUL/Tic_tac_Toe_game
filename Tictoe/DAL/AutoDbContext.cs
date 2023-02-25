using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data.SqlClient;
using Tictoe.Model;
using Microsoft.EntityFrameworkCore.SqlServer;
//using Microsoft.EntityFrameworkCore;
namespace Tictoe.DAL
{
    public class AutoDbContext: DbContext
    {
        public IConfiguration configuration;
        public SqlConnection sql;


        public AutoDbContext(DbContextOptions<AutoDbContext> options, IConfiguration _configuration) : base(options)
        {
            configuration = _configuration;
            string conn = ConfigurationExtensions.GetConnectionString(configuration, "Local_Conn");

            sql = new SqlConnection(conn);
        }
        public DbSet<User> userDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u._id);

            modelBuilder.Entity<User>()
                .Property(u => u.first_name)
                .IsRequired();

            // more configuration here for other entities
        }
    }


}
