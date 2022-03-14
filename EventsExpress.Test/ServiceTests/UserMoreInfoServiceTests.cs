namespace EventsExpress.Test.ServiceTests
{
    using System;
    using System.Threading.Tasks;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Core.Services;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class UserMoreInfoServiceTests : TestInitializer
    {
        private UserMoreInfoService service;

        private UserMoreInfoDto userMoreInfoDTO;

        private User user;
        private User validUserButNotVisitor;
        private UserMoreInfo userMoreInfo;

        private Guid userMoreInfoId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        private Guid validUserId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            service = new UserMoreInfoService(Context, MockMapper.Object);

            userMoreInfoDTO = new UserMoreInfoDto
            {
                Id = userMoreInfoId,
                UserId = userId,
                ParentStatus = ParentStatus.Kids,
                TheTypeOfLeisure = TheTypeOfLeisure.Passive,
                RelationShipStatus = RelationShipStatus.Single,
                AdditionalInfoAboutUser = "AditionalInfoAboutUser",
            };

            userMoreInfo = new UserMoreInfo
            {
                Id = userMoreInfoId,
                UserId = userId,
                ParentStatus = ParentStatus.Kids,
                TheTypeOfLeisure = TheTypeOfLeisure.Passive,
                RelationShipStatus = RelationShipStatus.Single,
                AdditionalInfo = "AditionalInfoAboutUser",
            };

            user = new User
            {
                Id = userId,
                Name = "Name",
                Email = "Email",
            };

            validUserButNotVisitor = new User
            {
                Id = validUserId,
                Name = "Name",
                Email = "Email",
            };

            Context.Users.AddRange(user, validUserButNotVisitor);
            Context.UserMoreInfo.Add(userMoreInfo);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<UserMoreInfoDto, UserMoreInfo>(It.IsAny<UserMoreInfoDto>()))
               .Returns((UserMoreInfoDto e) => e == null ?
               null :
               new UserMoreInfo()
               {
                   Id = e.Id,
                   UserId = e.UserId,
                   ParentStatus = e.ParentStatus,
                   TheTypeOfLeisure = e.TheTypeOfLeisure,
                   RelationShipStatus = e.RelationShipStatus,
                   AdditionalInfo = e.AdditionalInfoAboutUser,
               });
        }

        [Test]
        public void CreateAsync_ExistingObject_DoesNotThrowException()
        {
            var actual = service.CreateAsync(userMoreInfoDTO);

            Assert.IsInstanceOf<Task<Guid>>(actual);
        }
    }
}
