namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentsAcademies", "session", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentsAcademies", "session");
        }
    }
}
