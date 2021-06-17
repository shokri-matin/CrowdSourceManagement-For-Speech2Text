using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SegamDBContext: DbContext
    {
        public SegamDBContext():base("name = SegamDBContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<GanjineDBContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<STTContext, ConsoleApp1.Migrations.Configuration>());
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AllocatedFile> AllocatedFiles { get; set; }
        public DbSet<SpeechFile> SpeechFiles { get; set; }
        public DbSet<PersonActivityLog> PersonActivityLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<ExceptionLogging> ExceptionsLogging { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
