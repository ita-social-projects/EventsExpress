using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ITeamService
    {
        IEnumerable<Team> All();

        Task<OperationResult> AddTeamAsync(TeamDTO team);

        Task<OperationResult> AddDevAsync(DeveloperDTO developer);
    }
}
