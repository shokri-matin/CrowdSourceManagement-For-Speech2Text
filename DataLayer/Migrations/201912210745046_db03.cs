namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db03 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Person", new[] { "AdminPerson_PersonID" });
            RenameColumn(table: "dbo.Person", name: "AdminPerson_PersonID", newName: "CreatorId");
            AlterColumn("dbo.Person", "CreatorId", c => c.Int(nullable: true));
            CreateIndex("dbo.Person", "CreatorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Person", new[] { "CreatorId" });
            AlterColumn("dbo.Person", "CreatorId", c => c.Int());
            RenameColumn(table: "dbo.Person", name: "CreatorId", newName: "AdminPerson_PersonID");
            CreateIndex("dbo.Person", "AdminPerson_PersonID");
        }
    }
}
