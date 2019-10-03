using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flow.Models.Business
{
    interface IBusiness<T> where T : class
    {
        IList<T> FindAll();
        IList<T> FindBy(int id);
        long Save(T obj);
        void Edit(T obj);
        void Delete(T obj);
    }
}
