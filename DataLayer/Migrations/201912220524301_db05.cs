namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db05 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Person", new[] { "CreatorId" });
            AlterColumn("dbo.Person", "CreatorId", c => c.Int());
            CreateIndex("dbo.Person", "CreatorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Person", new[] { "CreatorId" });
            AlterColumn("dbo.Person", "CreatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Person", "CreatorId");
        }
    }
}
