using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project_data
{
    
    public class ApplicationUser : IdentityUser
    {

    }
    public class DbLMSContext : IdentityDbContext<ApplicationUser>
    {
        public DbLMSContext() : base("dbLMS")
        {
            var ensureDLLIsCopied =
             System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => !String.IsNullOrEmpty(type.Namespace))
               .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<SubmittedHomework> SubmittedHomework { get; set; }
        public DbSet<HomeWorkFile> HomeWorkFiles { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Employee> Employes { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<GroupProgram> GroupPrograms { get; set; }
        public DbSet<ClassName> ClassNames { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentsAcademy> StudentsAcademy { get; set; }
        public DbSet<InstituteInfo> InstituteInfos { get; set; }
        public DbSet<InstituteType> InstituteType { get; set; }
    }
    //public class AcademicSession
    //{
    //    public int Id { get; set; }
    //    public string session { get; set; }
    //    public string Description { get; set; }
    //}
    public class StudentsAcademy
    {
        public int ID { get; set; }
        [ForeignKey("Student")]
        public int StdId { get; set; }
        [ForeignKey("ClassName")]
        public int ClassID { get; set; }
        [ForeignKey("Section")]
        public int SectionID { get; set; }
        [ForeignKey("Shift")]
        public int ShiftID { get; set; }
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        [ForeignKey("GroupProgram")]
        public int GrpProgramID { get; set; }
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        public int STdYear { get; set; }
        public string session { get; set; }
        public GroupProgram GroupProgram { get; set; }
        public ClassName ClassName { get; set; }
        public Section Section { get; set; }
        public Shift Shift { get; set; }
        public Semester Semester { get; set; }
        public Department Department { get; set; }
        public Student Student { get; set; }
    }
    public class Homework
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [MaxLength]
        public string Description { get; set; }
        [ForeignKey("Employee")]
        public int TeacherID { get; set; }
        [ForeignKey("Subject")]
        public int SubId { get; set; }
        [ForeignKey("ClassName")]
        public int ClassId { get; set; }
        [ForeignKey("Section")]
        public int SecID { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        //public string Status { get; set; }
        public HomeworkStatus Status { get; set; } = HomeworkStatus.NotSubmitted;
        [NotMapped]
        public string Teacher { get; set; }
        [NotMapped]
        public string SubjectName { get; set; }
        [NotMapped]
        public string ClassNames { get; set; }
        [NotMapped]
        public string SectionName { get; set; }
        [NotMapped]
        public string ShiftName { get; set; }
        [NotMapped]
        public bool IsSubmitted { get; set; }
        public Employee Employee { get; set; }
        public Subject Subject { get; set; }
        public ClassName ClassName { get; set; }
        public Section Section { get; set; }
        public Shift Shift { get; set; }
        public ICollection<SubmittedHomework> SubmittedHomework { get; set; }
    }
    public enum HomeworkStatus
    {
        Submitted = 1,
        NotSubmitted = 2
    }
    public class SubmittedHomework
    {
        public int Id { get; set; }
        //public DateTime PublishedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittedDate { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [MaxLength]
        public string Description { get; set; }
        [ForeignKey("Homework")]
        public int HomeworkID { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }

        public string Status { get; set; }
        public Homework Homework { get; set; }
        public Student Student { get; set; }
        public ICollection<HomeWorkFile> HomeWorkFiles { get; set; }
    }
    public class HomeWorkFile
    {
        public int Id { get; set; }
        [ForeignKey("SubmittedHomework")]
        public int SubmittedHomeworkId { get; set; }
        public string AnswerPath { get; set; }
        public SubmittedHomework SubmittedHomework { get; set; }
    }
    public class EmployeeType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
    public class InstituteType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<InstituteInfo> InstituteInfos { get; set; }
    }
    public class InstituteInfo
    {
        public int Id { get; set; }
        public string InstituteName { get; set; }

        public string Logo { get; set; }
        [ForeignKey("InstituteType")]
        public int TypeID { get; set; }
        public InstituteType InstituteType { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JoiningDate { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string PresentAdd { get; set; }
        public string PermanentAdd { get; set; }
        [ForeignKey("Designation")]
        public int DesId { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public string Description { get; set; }
        [ForeignKey("EmployeeType")]
        public int TypeID { get; set; }
        public ICollection<Homework> HomeWorks { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
    }
    public class Designation
    {
        public int Id { get; set; }
        public string DesgName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int DeptId { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
    public class Subject
    {
        public int Id { get; set; }
        public string SubName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int GroupProgramId { get; set; }
        public string SubjectImage { get; set; }
    }
    public class GroupProgram
    {
        public int Id { get; set; }
        public string GroupProgramName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        [ForeignKey("InstituteType")]
        public int InstitteTypeID { get; set; }
        public InstituteType InstituteType { get; set; }
    }
    public class ClassName
    {
        public int Id { get; set; }
        public string ClassesName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

    }
    public class Shift
    {
        public int Id { get; set; }
        public string SiftName { get; set; }
    }
    public class Section
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
    public class Semester
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
    //public class SubmittedHomework
    //{
    //    public int Id { get; set; }
    //    public int HomeWorkId { get; set; }
    //    public int SubmitedBy { get; set; }
    //    public string StudentName { get; set; }
    //    public DateTime SubmittedDate { get; set; }
    //    public string SubmittedAnswer { get; set; }
    //    public string SubAttachment { get; set; }
    //}
    public class Comment
    {
        public int Id { get; set; }
        //[ForeignKey("Homework")]
        public int HomeWorkId { get; set; }
        public string Comments { get; set; }
        //[ForeignKey("Student")]
        public int StdId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentsDate { get; set; }
        //public Student Student { get; set; }
        //public Homework Homework { get; set; }
    }
    public enum Gender
    {
        Male = 1, Female, Other
    }
    public class Student
    {
      public int Id { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime DOB { get; set; }
      public string Name { get; set; }
      public string ContactNo { get; set; }
      public Gender Gender { get; set; }
      public string Email { get; set; }
      public string PresentAdd { get; set; }
      public string PermanentAdd { get; set; }
      public string ImagePath { get; set; }
      [NotMapped]
      public HttpPostedFileBase StudentPicture { get; set; }
      [ForeignKey("InstituteInfo")]
      public int InstId { get; set; }
      public InstituteInfo InstituteInfo { get; set; }
      public ICollection<SubmittedHomework> SubmittedHomeworks { get; set; }

   }
}
