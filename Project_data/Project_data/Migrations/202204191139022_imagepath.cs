namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "SubjectImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "SubjectImage");
        }
    }
}
