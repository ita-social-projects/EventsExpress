using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventsExpress.Test
{
    public class ConnectionFactory : IDisposable
    {
        private bool disposedValue = false;
        IHttpContextAccessor httpContextAccessor = new Mock<IHttpContextAccessor>().Object;

        public AppDbContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "EventsExpress").Options;
            var context = new AppDbContext(option, httpContextAccessor);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
