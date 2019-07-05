using EventsExpress.Db.EF;
using EventsExpress.Db.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EventsExpress.Db.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly AppDbContext Database;
        protected readonly DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            Database = context;
            entities = context.Set<T>();
        }


        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            entities.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new NotImplementedException();
            entities.Update(entity);
            return entity;
        }


        public IQueryable<T> Get()
        {
            return entities;
        }


        public T Get(Guid id)
        {
            return entities.Find(id);
        }


        public virtual IQueryable<T> Filter(int? skip = null,
          int? take = null,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeProperties = "")
        {
            IQueryable<T> query = entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            orderBy?.Invoke(query);

            if (skip != null && take != null)
                return query.Skip(skip.Value).Take(take.Value);
            return query;
        }                          
        
        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            entities.Remove(entity);
            return entity;
        }
    }
}
