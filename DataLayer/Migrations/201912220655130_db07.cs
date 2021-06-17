namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpeechFiles", "CreatorId", c => c.Int(nullable: false));
            AddColumn("dbo.SpeechFiles", "PublishedDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.SpeechFiles", "CreatorId");
            AddForeignKey("dbo.SpeechFiles", "CreatorId", "dbo.Person", "PersonID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpeechFiles", "CreatorId", "dbo.Person");
            DropIndex("dbo.SpeechFiles", new[] { "CreatorId" });
            DropColumn("dbo.SpeechFiles", "PublishedDate");
            DropColumn("dbo.SpeechFiles", "CreatorId");
        }
    }
}
