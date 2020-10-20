using AutoMapper;
using EventsExpress.Db.IRepo;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test
{
    [TestFixture]
    public abstract class TestInitializer
    {
        protected static Mock<IUnitOfWork> MockUnitOfWork { get; set; }

        protected static Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected virtual void Initialize()
        {
            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockMapper = new Mock<IMapper>();
            TestContext.WriteLine("Initialize test data");
        }

        [TearDown]
        protected virtual void Cleanup()
        {
            TestContext.WriteLine("Cleanup test data");
        }
    }
}
