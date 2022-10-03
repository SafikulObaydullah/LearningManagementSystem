using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface ISectionRepo : IRepository<Section>
    {

    }
    public class SectionRepo : BasicRepository<Section>, ISectionRepo
    {
        IUnitOfWork _unitOfWork;
        public SectionRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        string IRepository<Section>.Add(Section entity)
        {
            LmsContext.Sections.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<Section>.Delete(int Id)
        {
           var s = LmsContext.Sections.Find(Id);
            LmsContext.Sections.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Section> GetAll()
        {
           return LmsContext.Sections.ToList();
        }

        public Section GetById(int Id)
        {
            return LmsContext.Sections.Find(Id);
        }

        public int update(Section entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            LmsContext.Commit();
            return 1;
        }
    }
}
