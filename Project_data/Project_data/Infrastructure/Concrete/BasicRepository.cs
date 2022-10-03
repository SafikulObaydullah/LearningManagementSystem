using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Concrete
{
    //Concrete class  
    public abstract class BasicRepository<T> where T : class
    { 
        private DbLMSContext _lmsContext;
        private readonly IDbSet<T> dbset;
        //Property
        public DbLMSContext LmsContext
        {
            get { return _lmsContext ?? (_lmsContext = DatabaseFactory.Get()); }
        }
        protected IdatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }
        //Constructor(initialization)
        public BasicRepository(IdatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = LmsContext.Set<T>();
        }
       

    }
}
