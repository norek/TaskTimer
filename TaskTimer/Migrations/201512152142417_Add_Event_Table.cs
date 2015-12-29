namespace TaskTimer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Event_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
