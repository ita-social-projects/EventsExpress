using System;
using AutoMapper;
using EventsExpress.Db.EF;
using EventsExpress.Db.IBaseService;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        public BaseService(AppDbContext context, IMapper mapper = null)
        {
            Context = context;
            Mapper = mapper;
        }

        protected AppDbContext Context { get; set; }

        protected IMapper Mapper { get; set; }

        protected DbSet<T> Entities { get => Context.Set<T>(); }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Remove(entity);
            return entity;
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
