using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Infrastructure.Interfaces
{
    //To create a object of Context class
    public interface IdatabaseFactory : IDisposable
    {
        DbLMSContext Get();
    }
}
