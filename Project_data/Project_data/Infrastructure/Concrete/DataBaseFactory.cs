using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Concrete
{
   public class DataBaseFactory : Disposable, IdatabaseFactory
    {
        private DbLMSContext dbLMSContext;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public DbLMSContext Get()
        {
            return dbLMSContext ?? (dbLMSContext = new DbLMSContext());
        }
        public override void DisposeCore()
        {
            if(dbLMSContext != null)
            {
                dbLMSContext.Dispose();
            }
        }
    }
}
