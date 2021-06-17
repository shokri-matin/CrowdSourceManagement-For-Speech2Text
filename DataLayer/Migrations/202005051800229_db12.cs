namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocatedFiles", "Gender", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllocatedFiles", "Gender");
        }
    }
}
