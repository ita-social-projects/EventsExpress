using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventsExpress.Db.EF;
using EventsExpress.Db.IBaseService;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Db.BaseService
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        public BaseService(AppDbContext context)
        {
            Database = context;
            Entities = context.Set<T>();
        }

        protected DbSet<T> Entities { get; }

        protected AppDbContext Database { get; }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Remove(entity);
            return entity;
        }

        public T Get(Guid id)
        {
            return Entities.Find(id);
        }

        public IQueryable<T> Get(string includeProperties = "")
        {
            IQueryable<T> query = Entities;
            foreach (var includeProperty in
                includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Update(entity);
            return entity;
        }
    }
}
