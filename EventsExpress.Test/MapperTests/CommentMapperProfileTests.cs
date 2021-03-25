using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class CommentMapperProfileTests : MapperTestInitializer<CommentMapperProfile>
    {
        private CommentDto commentDto = new CommentDto
        {
            Id = Guid.NewGuid(),
            Text = "text",
            UserId = Guid.NewGuid(),
            EventId = Guid.NewGuid(),
            Date = new DateTime(2020, 01, 01),
            User = new Db.Entities.User
            {
                Id = Guid.NewGuid(),
                Name = "user",
            },
            CommentsId = Guid.NewGuid(),
        };

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();

            IServiceCollection services = new ServiceCollection();
            var mock = new Mock<IPhotoService>();
            services.AddTransient<IPhotoService>(sp => mock.Object);

            services.AddAutoMapper(typeof(CommentMapperProfile));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Mapper = serviceProvider.GetService<IMapper>();
            mock.Setup(x => x.GetPhotoFromAzureBlob(It.IsAny<string>())).Returns(Task.FromResult("test"));
        }

        [Test]
        public void CommentMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void CommentMapperProfile_CommentDtoToCommentViewModel()
        {
            var commentViewModel = Mapper.Map<CommentDto, CommentViewModel>(commentDto);

            Assert.That(commentViewModel.Id, Is.EqualTo(commentDto.Id));
            Assert.That(commentViewModel.UserName, Is.EqualTo(commentDto.User.Name ??
                commentDto.User.Email.Substring(0, commentDto.User.Email.IndexOf("@", StringComparison.Ordinal))));
            Assert.That(commentViewModel.Text, Is.EqualTo(commentDto.Text));
            Assert.That(commentViewModel.UserId, Is.EqualTo(commentDto.UserId));
            Assert.That(commentViewModel.EventId, Is.EqualTo(commentDto.EventId));
            Assert.That(commentViewModel.Date, Is.EqualTo(commentDto.Date));
            Assert.That(commentViewModel.CommentsId, Is.EqualTo(commentDto.CommentsId));
            Assert.That(commentViewModel.Children, Is.Empty);
            Assert.That(commentViewModel.UserPhoto, Is.EqualTo("test"));
        }
    }
}
