namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllocatedFiles",
                c => new
                    {
                        FileID = c.Guid(nullable: false),
                        TaskID = c.Guid(nullable: false),
                        Text = c.String(),
                        IsSubmited = c.Boolean(nullable: false),
                        SubmitedTime = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.FileID, t.TaskID })
                .ForeignKey("dbo.SpeechFiles", t => t.FileID, cascadeDelete: true)
                .ForeignKey("dbo.Task", t => t.TaskID, cascadeDelete: true)
                .Index(t => t.FileID)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.SpeechFiles",
                c => new
                    {
                        FileID = c.Guid(nullable: false),
                        FileName = c.String(nullable: false),
                        ParentID = c.String(nullable: false),
                        GroupID = c.Int(nullable: false),
                        FileType = c.String(nullable: false),
                        FileDuration = c.Int(nullable: false),
                        SuggestedText = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskID = c.Guid(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        StartTaskDate = c.DateTime(nullable: false),
                        EndTaskDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        PersonName = c.String(nullable: false, maxLength: 100),
                        PersonEmail = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Boolean(nullable: false),
                        RoleID = c.Int(nullable: false),
                        AdminPerson_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Person", t => t.AdminPerson_PersonID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.AdminPerson_PersonID);
            
            CreateTable(
                "dbo.PersonActivityLog",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        ActivityStatus = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllocatedFiles", "TaskID", "dbo.Task");
            DropForeignKey("dbo.Task", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Person", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.PersonActivityLog", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Person", "AdminPerson_PersonID", "dbo.Person");
            DropForeignKey("dbo.SpeechFiles", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.AllocatedFiles", "FileID", "dbo.SpeechFiles");
            DropIndex("dbo.PersonActivityLog", new[] { "PersonID" });
            DropIndex("dbo.Person", new[] { "AdminPerson_PersonID" });
            DropIndex("dbo.Person", new[] { "RoleID" });
            DropIndex("dbo.Task", new[] { "PersonID" });
            DropIndex("dbo.SpeechFiles", new[] { "GroupID" });
            DropIndex("dbo.AllocatedFiles", new[] { "TaskID" });
            DropIndex("dbo.AllocatedFiles", new[] { "FileID" });
            DropTable("dbo.Roles");
            DropTable("dbo.PersonActivityLog");
            DropTable("dbo.Person");
            DropTable("dbo.Task");
            DropTable("dbo.Groups");
            DropTable("dbo.SpeechFiles");
            DropTable("dbo.AllocatedFiles");
        }
    }
}
