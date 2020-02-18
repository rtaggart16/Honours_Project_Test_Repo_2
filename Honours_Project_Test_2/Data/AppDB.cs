using Honours_Project_Test_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TalkAPI.Data
{
    public class AppDB : IdentityDbContext<User>
    {
        /*
         * - Categorisation
         *      * Session
         *      * Tag
         * - Talk
         *      * Talk
         *      * Talk_Session
         *      * Talk_Tag
         *      * User_Talk_Rating
         * - User
         *      * Author
        */

        // Categorisation

        public DbSet<Author> Authors { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        // END: Categorisation

        // User

        //public DbSet<Author> Authors { get; set; }

        // END: User


        // Talk

        public DbSet<User_Talk_Rating> User_Talk_Ratings { get; set; }

        public DbSet<User_Talk_Interest> User_Talk_Interests { get; set; }

        public DbSet<Talk> Talks { get; set; }

        public DbSet<Talk_Session> Talk_Sessions { get; set; }

        public DbSet<Talk_Tag> Talk_Tags { get; set; }

        // END: Talk


        public AppDB(DbContextOptions<AppDB> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }

            base.OnModelCreating(builder);
        }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<AppDB>
    {
        public AppDB CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDB>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              //Get correct root path
              .SetBasePath(Directory.GetCurrentDirectory().ToString())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new AppDB(builder.Options);
        }
    }

    public static class TalkAPIDBExtensions
    {
        public static void SeedDB(this AppDB _db, IServiceProvider serviceProvider, UserManager<User> userManager)
        {
        }
    }

}
