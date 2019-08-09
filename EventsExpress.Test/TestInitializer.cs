using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using EventsExpress.Db.IRepo;


namespace EventsExpress.Test
{
    [TestFixture]
    public abstract class TestInitializer
    {
        protected static Mock<IUnitOfWork> mockUnitOfWork;
        protected static Mock<IMapper> mockMapper;
       


        [SetUp]
        protected virtual void Initialize()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMapper = new Mock<IMapper>();
            
            TestContext.WriteLine("Initialize test data");
        }

        [TearDown]
        protected virtual void Cleanup()
        {
            TestContext.WriteLine("Cleanup test data");
        }

    }
}
