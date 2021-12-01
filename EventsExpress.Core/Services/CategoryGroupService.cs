using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CategoryGroupService : BaseService<CategoryGroup>, ICategoryGroupService
    {
        public CategoryGroupService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public bool Exists(Guid id) =>
            Context.CategoryGroups.Any(x => x.Id == id);

        public IEnumerable<CategoryGroupDto> GetAllGroups()
        {
            var groups = Context.CategoryGroups
                                .Include(cg => cg.Categories)
                                .ProjectTo<CategoryGroupDto>(Mapper.ConfigurationProvider)
                                .OrderBy(cg => cg.Title);

            return groups;
        }

        public CategoryGroupDto GetById(Guid id)
        {
            var res = Context.CategoryGroups.Find(id);

            return Mapper.Map<CategoryGroup, CategoryGroupDto>(res);
        }
    }
}
