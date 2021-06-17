namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExceptionsLogging", "LogDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionsLogging", "LogDate");
        }
    }
}
