using EventsExpress.Db.Entities;
using System;
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

        IEventScheduleRepository EventScheduleRepository { get; }

        IPermissionRepository PermissionRepository { get; }

        IPhotoRepository PhotoRepository { get; }

        IRateRepository RateRepository { get; }

        IRelationshipRepository RelationshipRepository { get; }

        IReportRepository ReportRepository { get; }

        IRoleRepository RoleRepository { get; }

        IUserRepository UserRepository { get; }

        IChatRepository ChatRepository { get; }

        IMessageRepository MessageRepository { get; }

        IRepository<EventOwner> EventOwnersRepository { get; }

        IInventoryRepository InventoryRepository { get; }

        IUnitOfMeasuringRepository UnitOfMeasuringRepository { get; }

        IEventStatusHistoryRepository EventStatusHistoryRepository { get; }

        IUserEventRepository UserEventRepository { get; }

        Task SaveAsync();
    }
}
