using EventsExpress.Db.Entities;     
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Db.IRepo
{
    public interface IUnitOfWork : IDisposable
    {
         ICategoryRepository CategoryRepository { get; }
         ICityRepository CityRepository { get; }
         ICommentsRepository CommentsRepository { get; }
         ICountryRepository CountryRepository { get; }
         IEventRepository EventRepository { get; }
         IPermissionRepository PermissionRepository { get; }
         IPhotoRepository PhotoRepository { get; }
         IRateRepository RateRepository { get; }
         IRelationshipRepository RelationshipRepository { get; }
         IReportRepository ReportRepository { get; }
         IRoleRepository RoleRepository { get; }
         IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
