namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db09 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SpeechFiles", "FileDuration", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpeechFiles", "FileDuration", c => c.Int(nullable: false));
        }
    }
}
