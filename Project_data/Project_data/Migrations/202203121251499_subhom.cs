namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subhom : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Homework", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Homework", "Status", c => c.String());
        }
    }
}
