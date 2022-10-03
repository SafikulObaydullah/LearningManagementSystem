using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Concrete
{
    public class Disposable : IDisposable
    {

        public bool isDisposed;


        public Disposable()
        {
            //Constructor
            //ClassName
            //No ReurnType 

        }
        ~Disposable()
        {
            //Destructor
            Dispose(false);
        }
        //MethodOverloading(Dispose(),Dispose(bool disposing)
        //same method name but pARAmeter signature different)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }
            isDisposed = true;
        }


        public virtual void DisposeCore()
        {

        }
    }
}
