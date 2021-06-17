namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpeechFiles", "SequenceID", c => c.String(nullable: false));
            DropColumn("dbo.SpeechFiles", "ParentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpeechFiles", "ParentID", c => c.String(nullable: false));
            DropColumn("dbo.SpeechFiles", "SequenceID");
        }
    }
}
