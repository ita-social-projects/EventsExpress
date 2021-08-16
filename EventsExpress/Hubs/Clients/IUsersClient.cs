using System.Threading.Tasks;

namespace EventsExpress.Hubs.Clients
{
    public interface IUsersClient
    {
        Task CountUsers();
    }
}
