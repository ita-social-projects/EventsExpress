
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Db.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext Database;

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

        public UnitOfWork(
            AppDbContext context
            )
        {
            Database = context;                   
        }

        public IChatRepository ChatRepository =>
           _chatRepository ?? (_chatRepository = new ChatRepository(Database));   

        public IMessageRepository MessageRepository =>
           _messageRepository ?? (_messageRepository = new MessageRepository(Database));

        public IEventRepository EventRepository =>
           _eventRepository ?? (_eventRepository = new EventRepository(Database));

        public ICategoryRepository CategoryRepository =>
           _categoryRepository ?? (_categoryRepository = new CategoryRepository(Database));

        public ICityRepository CityRepository =>
            _cityRepository ?? (_cityRepository = new CityRepository(Database));

        public ICountryRepository CountryRepository =>
            _countryRepository ?? (_countryRepository = new CountryRepository(Database));

        public ICommentsRepository CommentsRepository =>
            _commentsRepository ?? (_commentsRepository = new CommentsRepository(Database));

        public IPermissionRepository PermissionRepository =>
            _permissionRepository ?? (_permissionRepository = new PermissionRepository(Database));

        public IPhotoRepository PhotoRepository =>
            _photoRepository ?? (_photoRepository = new PhotoRepository(Database));

        public IRateRepository RateRepository =>
            _rateRepository ?? (_rateRepository = new RateRepository(Database));

        public IRelationshipRepository RelationshipRepository =>
            _relationshipRepository ?? (_relationshipRepository = new RelationshipRepository(Database));

        public IReportRepository ReportRepository =>
            _reportRepository ?? (_reportRepository = new ReportRepository(Database));

        public IRoleRepository RoleRepository =>
            _roleRepository ?? (_roleRepository = new RoleRepository(Database));

        public IUserRepository UserRepository =>
            _userRepository ?? (_userRepository = new UserRepository(Database));







        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

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
            await Database.SaveChangesAsync();
        }
    }
}
