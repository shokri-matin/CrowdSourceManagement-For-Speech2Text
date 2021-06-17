namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionsLogging",
                c => new
                    {
                        LogId = c.Guid(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        Exception = c.String(),
                        InnerException = c.String(),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExceptionsLogging", "PersonID", "dbo.Person");
            DropIndex("dbo.ExceptionsLogging", new[] { "PersonID" });
            DropTable("dbo.ExceptionsLogging");
        }
    }
}
