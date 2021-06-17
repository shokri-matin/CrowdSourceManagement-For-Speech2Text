namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllocatedFiles", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AllocatedFiles", "Gender", c => c.Boolean());
        }
    }
}
