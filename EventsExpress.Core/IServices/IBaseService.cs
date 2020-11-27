using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsExpress.Db.IBaseService
{
    public interface IBaseService<T>
        where T : class
    {
        T Insert(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
