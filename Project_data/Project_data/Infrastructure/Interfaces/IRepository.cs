using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        string Add(T entity);
        int update(T entity);
        int Delete(int Id);
        T GetById(int Id);
        IEnumerable<T> GetAll();
    }
}
