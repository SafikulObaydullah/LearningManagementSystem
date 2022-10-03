namespace Project_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassesName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeWorkId = c.Int(nullable: false),
                        Comments = c.String(),
                        StdId = c.Int(nullable: false),
                        CommentsDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesgName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        DeptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DOB = c.DateTime(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 50),
                        ContactNo = c.String(),
                        Email = c.String(),
                        PresentAdd = c.String(),
                        PermanentAdd = c.String(),
                        DesId = c.Int(nullable: false),
                        DeptId = c.Int(nullable: false),
                        Description = c.String(),
                        TypeID = c.Int(nullable: false),
                        EmployeeType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeType_Id)
                .Index(t => t.EmployeeType_Id);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        Id = c.Int(nullable: true, identity: true),
                        PublishedDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Title = c.String(maxLength: 200),
                        Description = c.String(),
                        TeacherID = c.Int(nullable: false),
                        SubId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        SecID = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassNames", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.TeacherID, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SecID, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubId, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.SubId)
                .Index(t => t.ClassId)
                .Index(t => t.SecID)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiftName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        GroupProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupProgramName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        InstitteTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstituteTypes", t => t.InstitteTypeID, cascadeDelete: true)
                .Index(t => t.InstitteTypeID);
            
            CreateTable(
                "dbo.InstituteTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstituteInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(),
                        Logo = c.String(),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstituteTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.HomeWorkFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubmittedHomeworkId = c.Int(nullable: false),
                        AnswerPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubmittedHomeworks", t => t.SubmittedHomeworkId, cascadeDelete: true)
                .Index(t => t.SubmittedHomeworkId);
            
            CreateTable(
                "dbo.SubmittedHomeworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubmittedDate = c.DateTime(nullable: false),
                        Title = c.String(maxLength: 200),
                        Description = c.String(),
                        HomeworkID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Homework", t => t.HomeworkID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.HomeworkID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DOB = c.DateTime(nullable: false),
                        Name = c.String(),
                        ContactNo = c.String(),
                        Gender = c.Int(nullable: false),
                        Email = c.String(),
                        PresentAdd = c.String(),
                        PermanentAdd = c.String(),
                        ImagePath = c.String(),
                        InstId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstituteInfoes", t => t.InstId, cascadeDelete: false)
                .Index(t => t.InstId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SemesterName = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentsAcademies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StdId = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                        SectionID = c.Int(nullable: false),
                        ShiftID = c.Int(nullable: false),
                        DeptID = c.Int(nullable: false),
                        GrpProgramID = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        STdYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassNames", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptID, cascadeDelete: true)
                .ForeignKey("dbo.GroupPrograms", t => t.GrpProgramID, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StdId, cascadeDelete: true)
                .Index(t => t.StdId)
                .Index(t => t.ClassID)
                .Index(t => t.SectionID)
                .Index(t => t.ShiftID)
                .Index(t => t.DeptID)
                .Index(t => t.GrpProgramID)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentsAcademies", "StdId", "dbo.Students");
            DropForeignKey("dbo.StudentsAcademies", "ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.StudentsAcademies", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.StudentsAcademies", "SectionID", "dbo.Sections");
            DropForeignKey("dbo.StudentsAcademies", "GrpProgramID", "dbo.GroupPrograms");
            DropForeignKey("dbo.StudentsAcademies", "DeptID", "dbo.Departments");
            DropForeignKey("dbo.StudentsAcademies", "ClassID", "dbo.ClassNames");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.HomeWorkFiles", "SubmittedHomeworkId", "dbo.SubmittedHomeworks");
            DropForeignKey("dbo.SubmittedHomeworks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "InstId", "dbo.InstituteInfoes");
            DropForeignKey("dbo.SubmittedHomeworks", "HomeworkID", "dbo.Homework");
            DropForeignKey("dbo.GroupPrograms", "InstitteTypeID", "dbo.InstituteTypes");
            DropForeignKey("dbo.InstituteInfoes", "TypeID", "dbo.InstituteTypes");
            DropForeignKey("dbo.Homework", "SubId", "dbo.Subjects");
            DropForeignKey("dbo.Homework", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Homework", "SecID", "dbo.Sections");
            DropForeignKey("dbo.Homework", "TeacherID", "dbo.Employees");
            DropForeignKey("dbo.Homework", "ClassId", "dbo.ClassNames");
            DropForeignKey("dbo.Employees", "EmployeeType_Id", "dbo.EmployeeTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StudentsAcademies", new[] { "SemesterId" });
            DropIndex("dbo.StudentsAcademies", new[] { "GrpProgramID" });
            DropIndex("dbo.StudentsAcademies", new[] { "DeptID" });
            DropIndex("dbo.StudentsAcademies", new[] { "ShiftID" });
            DropIndex("dbo.StudentsAcademies", new[] { "SectionID" });
            DropIndex("dbo.StudentsAcademies", new[] { "ClassID" });
            DropIndex("dbo.StudentsAcademies", new[] { "StdId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Students", new[] { "InstId" });
            DropIndex("dbo.SubmittedHomeworks", new[] { "StudentID" });
            DropIndex("dbo.SubmittedHomeworks", new[] { "HomeworkID" });
            DropIndex("dbo.HomeWorkFiles", new[] { "SubmittedHomeworkId" });
            DropIndex("dbo.InstituteInfoes", new[] { "TypeID" });
            DropIndex("dbo.GroupPrograms", new[] { "InstitteTypeID" });
            DropIndex("dbo.Homework", new[] { "ShiftId" });
            DropIndex("dbo.Homework", new[] { "SecID" });
            DropIndex("dbo.Homework", new[] { "ClassId" });
            DropIndex("dbo.Homework", new[] { "SubId" });
            DropIndex("dbo.Homework", new[] { "TeacherID" });
            DropIndex("dbo.Employees", new[] { "EmployeeType_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StudentsAcademies");
            DropTable("dbo.Semesters");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.SubmittedHomeworks");
            DropTable("dbo.HomeWorkFiles");
            DropTable("dbo.InstituteInfoes");
            DropTable("dbo.InstituteTypes");
            DropTable("dbo.GroupPrograms");
            DropTable("dbo.Subjects");
            DropTable("dbo.Shifts");
            DropTable("dbo.Sections");
            DropTable("dbo.Homework");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Designations");
            DropTable("dbo.Departments");
            DropTable("dbo.Comments");
            DropTable("dbo.ClassNames");
        }
    }
}
