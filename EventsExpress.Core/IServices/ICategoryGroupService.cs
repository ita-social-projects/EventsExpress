using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface ICategoryGroupService
    {
        IEnumerable<CategoryGroupDto> GetAllGroups();

        CategoryGroupDto GetById(Guid id);

        bool Exists(Guid id);
    }
}
