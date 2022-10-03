using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using Project_data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Helper
{
    public class RepositoryAccess
    {
        private IdatabaseFactory dbFactory;
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }
        public RepositoryAccess()
        {
            this.dbFactory = new DataBaseFactory();
            this.unitOfWork = new UnitOfWork(dbFactory);
        }
        public ISubjectRepo Subject { get { return new SubjectRepo(dbFactory, UnitOfWork); } }
        public IShiftRepo Shift { get { return new ShiftRepo(dbFactory, UnitOfWork); } }
        public ISectionRepo sectionRepo { get { return new SectionRepo(dbFactory, UnitOfWork); } }
        public ISemesterRepo semesterRepo { get { return new SemesterRepo(dbFactory, UnitOfWork); } }
        public IGroupRepo groupRepo { get { return new GroupProgramRepo(dbFactory, UnitOfWork); } }
        public IClassNameRepo classNameRepo { get { return new ClassNameRepo(dbFactory, UnitOfWork); } }
        public ISubmittedHomeworkRepo SubmittedHomeworkRepo { get { return new SubmittedHomeworkRepo(dbFactory, UnitOfWork); } }
        public IDesignationRepo DesignationRepo { get { return new DesignationRepo(dbFactory, UnitOfWork); } }
        public IDepartmentRepo DepartmentRepo { get { return new DepartmrntRepo(dbFactory, UnitOfWork); } }
        public IEmployeeRepo EmployeeRepo { get { return new EmployeeRepo(dbFactory, UnitOfWork); } }
        public IEmployeeTypeRepo EmployeeTypeRepo { get { return new EmployeeTypeRepo(dbFactory, UnitOfWork); } }
        public IHomeWorkRepo HomeworkRepo { get { return new HomeWorkRepo(dbFactory, UnitOfWork); } }
        public IHomeWorkFileRepo HomeWorkFileRepo { get { return new HomeWorkFileRepo(dbFactory, UnitOfWork); } }
        public IStudentRepo StudentRepo { get { return new StudentRepo(dbFactory, UnitOfWork); } }
        public IStudentsAcademyRepo StudentsAcademyRepo { get { return new StudentsAcademyRepo(dbFactory, UnitOfWork); } }
        public IInstituteInfoRepo InstituteInfoRepo { get { return new InstituteInfoRepo(dbFactory, UnitOfWork); } }
        public IInstituteType InstituteTypeRepo { get { return new InstituteTypeRepo(dbFactory, UnitOfWork); } }
        public ICommentRepo CommentRepo { get { return new CommentRepo(dbFactory, unitOfWork); } }

    }
}
