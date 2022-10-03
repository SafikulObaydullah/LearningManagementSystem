using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IShiftRepo: IRepository<Shift>
    {

    }
    public class ShiftRepo : BasicRepository<Shift>,IShiftRepo
    {
        IUnitOfWork _unitOfWork;
        public ShiftRepo(IdatabaseFactory idatabaseFactory, IUnitOfWork unitOfWork) :
         base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Shift> GetAll()
        {
            return LmsContext.Shifts.ToList();
        }
        public Shift GetById(int Id)
        {
            return LmsContext.Shifts.Find(Id);
        }
        string IRepository<Shift>.Add(Shift entity)
        {
            LmsContext.Shifts.Add(entity);
            return _unitOfWork.Commit();
        }

         int IRepository<Shift>.Delete(int Id)
        {
            var s = LmsContext.Shifts.Find(Id);
            LmsContext.Shifts.Remove(s);
            return 1;
        }
        int IRepository<Shift>.update(Shift entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
   
}
