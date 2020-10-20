using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
