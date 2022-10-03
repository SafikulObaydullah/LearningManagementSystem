namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsDes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Employees", "DesId");
            CreateIndex("dbo.Employees", "DeptId");
            AddForeignKey("dbo.Employees", "DeptId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "DesId", "dbo.Designations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DesId", "dbo.Designations");
            DropForeignKey("dbo.Employees", "DeptId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DeptId" });
            DropIndex("dbo.Employees", new[] { "DesId" });
        }
    }
}
