namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db131 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedFiles", "IsSupervised", c => c.Boolean(nullable: false));
            DropColumn("dbo.AllocatedFiles", "IsSupervied");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AllocatedFiles", "IsSupervied", c => c.Boolean(nullable: false));
            DropColumn("dbo.AllocatedFiles", "IsSupervised");
        }
    }
}
