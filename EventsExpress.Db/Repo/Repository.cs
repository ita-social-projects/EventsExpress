using System;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.IRepo;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Db.Repo
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(AppDbContext context)
        {
            Database = context;
            Entities = context.Set<T>();
        }

        protected DbSet<T> Entities { get; }

        protected AppDbContext Database { get; }

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

        public T Get(Guid id)
        {
            return Entities.Find(id);
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Remove(entity);
            return entity;
        }
    }
}
