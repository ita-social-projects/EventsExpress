using AutoMapper;
using EventsExpress.Db.EF;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test
{
    [TestFixture]
    public abstract class TestInitializer
    {
        protected static Mock<IMapper> MockMapper { get; set; }

        protected AppDbContext Context { get; set; }

        [SetUp]
        protected virtual void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            var factory = new ConnectionFactory();
            Context = factory.CreateContextForInMemory();
            TestContext.WriteLine("Initialize test data");
        }

        [TearDown]
        protected virtual void Cleanup()
        {
            TestContext.WriteLine("Cleanup test data");
            Context.Dispose();
        }
    }
}
