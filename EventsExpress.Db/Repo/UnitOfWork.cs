using System;
using System.Threading.Tasks;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext database;

        private IEventRepository _eventRepository;
        private ICategoryRepository _categoryRepository;
        private ICityRepository _cityRepository;
        private ICountryRepository _countryRepository;
        private ICommentsRepository _commentsRepository;
        private IPermissionRepository _permissionRepository;
        private IPhotoRepository _photoRepository;
        private IRateRepository _rateRepository;
        private IRelationshipRepository _relationshipRepository;
        private IReportRepository _reportRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IChatRepository _chatRepository;
        private IMessageRepository _messageRepository;
        private IRepository<EventOwner> _eventOwnersRepository;
        private bool disposed = false;

        public UnitOfWork(
            AppDbContext context)
        {
            database = context;
        }

        public IChatRepository ChatRepository =>
           _chatRepository ?? (_chatRepository = new ChatRepository(database));

        public IMessageRepository MessageRepository =>
           _messageRepository ?? (_messageRepository = new MessageRepository(database));

        public IEventRepository EventRepository =>
           _eventRepository ?? (_eventRepository = new EventRepository(database));

        public ICategoryRepository CategoryRepository =>
           _categoryRepository ?? (_categoryRepository = new CategoryRepository(database));

        public ICityRepository CityRepository =>
            _cityRepository ?? (_cityRepository = new CityRepository(database));

        public ICountryRepository CountryRepository =>
            _countryRepository ?? (_countryRepository = new CountryRepository(database));

        public ICommentsRepository CommentsRepository =>
            _commentsRepository ?? (_commentsRepository = new CommentsRepository(database));

        public IPermissionRepository PermissionRepository =>
            _permissionRepository ?? (_permissionRepository = new PermissionRepository(database));

        public IPhotoRepository PhotoRepository =>
            _photoRepository ?? (_photoRepository = new PhotoRepository(database));

        public IRateRepository RateRepository =>
            _rateRepository ?? (_rateRepository = new RateRepository(database));

        public IRelationshipRepository RelationshipRepository =>
            _relationshipRepository ?? (_relationshipRepository = new RelationshipRepository(database));

        public IReportRepository ReportRepository =>
            _reportRepository ?? (_reportRepository = new ReportRepository(database));

        public IRoleRepository RoleRepository =>
            _roleRepository ?? (_roleRepository = new RoleRepository(database));

        public IUserRepository UserRepository =>
            _userRepository ?? (_userRepository = new UserRepository(database));

        public IRepository<EventOwner> EventOwnersRepository => 
            _eventOwnersRepository ?? (_eventOwnersRepository = new Repository<EventOwner>(database));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                this.disposed = true;
            }
        }

        public async Task SaveAsync()
        {
            await database.SaveChangesAsync();
        }
    }
}
