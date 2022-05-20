using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests;

[TestFixture]
internal class UserMoreInfoServiceTests : TestInitializer
{
    private UserMoreInfoService service;

    private UserMoreInfoDto userMoreInfoDto;

    [SetUp]
    protected override void Initialize()
    {
        base.Initialize();

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "FirstName",
            Email = "Email",
        };

        userMoreInfoDto = new UserMoreInfoDto
        {
            UserId = user.Id,
            ParentStatus = ParentStatus.Kids,
            TheTypeOfLeisure = TheTypeOfLeisure.Passive,
            RelationShipStatus = RelationShipStatus.Single,
            AdditionalInfo = "AdditionalInfoAboutUser",
        };

        Context.Users.Add(user);
        Context.SaveChanges();

        var securityContextMock = new Mock<ISecurityContext>();
        service = new UserMoreInfoService(Context, MockMapper.Object, securityContextMock.Object);

        MockMapper.Setup(u => u.Map<UserMoreInfo>(It.IsAny<UserMoreInfoDto>()))
            .Returns((UserMoreInfoDto e) =>
                e == null
                ? null
                : new UserMoreInfo
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    ParentStatus = e.ParentStatus,
                    TheTypeOfLeisure = e.TheTypeOfLeisure,
                    RelationShipStatus = e.RelationShipStatus,
                    AdditionalInfo = e.AdditionalInfo,
                });

        securityContextMock.Setup(sc => sc.GetCurrentUserId()).Returns(user.Id);
    }

    [Test]
    public void CreateAsync_ValidObject_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await service.CreateAsync(userMoreInfoDto);
        });
    }

    [Test]
    public void CreateAsync_ExistingUser_ThrowsEventsExpressException()
    {
        Assert.ThrowsAsync<EventsExpressException>(async () =>
        {
            for (int i = 0; i < 2; i++)
            {
                await service.CreateAsync(userMoreInfoDto);
            }
        });
    }
}
