using System;

namespace EventsExpress.Db.Bridge
{
    public interface ISecurityContext
    {
        Guid GetCurrentUserId();

        Guid GetCurrentAccountId();

        bool UserIsAuthentificated();
    }
}
