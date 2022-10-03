using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbLMSContext _lMSContext;
        private readonly IdatabaseFactory _idatabaseFactory;
        public UnitOfWork(IdatabaseFactory idatabaseFactory)
        {
            this._idatabaseFactory = idatabaseFactory;
        }
        public DbLMSContext DbLMSContext
        {
            get
            {
                return _lMSContext ?? (_lMSContext = _idatabaseFactory.Get());
            }
        }
        public string Commit()
        {
            string msg = "";
            try
            {
               int result = DbLMSContext.SaveChanges();
                DbLMSContext.Commit();
                if(result>0)
                {
                    msg = "Successfully save";
                }
            }
            catch(DbEntityValidationException ex)
            {
                // throw new DbEntityValidationException(DisplayError(ex));
                msg = DisplayError(ex);
            }
            catch(Exception ex)
            {
               msg =  DisplayError(ex);
            }
            return msg;
        }
        private string DisplayError(DbEntityValidationException ex)
        {
            string msg = "";
            foreach (var error in ex.EntityValidationErrors)
            {
                foreach (var m in error.ValidationErrors)
                {
                    msg += $"(Property :{m.PropertyName} error : {m.ErrorMessage})\n";
                }
            }
            return msg;
        }
        private string DisplayError(Exception ex)
        {
            string msg = "";
            msg += ex.Message;
            return msg;
        } 
    }
}
