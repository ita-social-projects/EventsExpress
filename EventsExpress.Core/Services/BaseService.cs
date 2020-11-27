using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using EventsExpress.Db.EF;
using EventsExpress.Db.IBaseService;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Db.BaseService
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        public BaseService(AppDbContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        protected DbSet<T> Entities { get => _context.Set<T>(); }

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
