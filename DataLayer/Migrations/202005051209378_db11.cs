namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedFiles", "IsSupervied", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllocatedFiles", "IsSupervied");
        }
    }
}
