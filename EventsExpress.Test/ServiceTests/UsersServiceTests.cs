using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AutoMapper;
using EventsExpress.Core.IServices;
using MediatR;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Test.ServiceTests
{    [TestFixture]
    class UsersServiceTests: TestInitializer
    {
        private UserService service;
        private User user;

        private static Mock<IPhotoService> mockPhotoService;
        private static Mock<IMediator> mockMediator;
        private static Mock<IEmailService> mockEmailService;
        private static Mock<CacheHelper> mockCacheHelper;

        [SetUp]
        protected override void Initialize()
        {
            mockMediator = new Mock<IMediator>();
            mockPhotoService = new Mock<IPhotoService>();
            mockEmailService = new Mock<IEmailService>();
            mockCacheHelper = new Mock<CacheHelper>();

            service = new UserService(mockUnitOfWork.Object, mockMapper.Object, mockPhotoService.Object, mockMediator.Object, mockCacheHelper.Object, mockEmailService.Object);
           

        }

        
    }

}
