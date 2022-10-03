namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Typeid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "EmployeeType_Id", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "EmployeeType_Id" });
            DropColumn("dbo.Employees", "TypeID");
            RenameColumn(table: "dbo.Employees", name: "EmployeeType_Id", newName: "TypeID");
            AlterColumn("dbo.Employees", "TypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "TypeID");
            AddForeignKey("dbo.Employees", "TypeID", "dbo.EmployeeTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TypeID", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "TypeID" });
            AlterColumn("dbo.Employees", "TypeID", c => c.Int());
            RenameColumn(table: "dbo.Employees", name: "TypeID", newName: "EmployeeType_Id");
            AddColumn("dbo.Employees", "TypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "EmployeeType_Id");
            AddForeignKey("dbo.Employees", "EmployeeType_Id", "dbo.EmployeeTypes", "Id");
        }
    }
}
