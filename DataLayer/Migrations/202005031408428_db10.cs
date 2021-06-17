namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpeechFiles", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.SpeechFiles", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpeechFiles", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.SpeechFiles", "IsActive");
        }
    }
}
