using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    public class NotificationTemplateMapperTests : MapperTestInitializer<NotificationTemplateMapperProfile>
    {
        private const NotificationProfile Id = NotificationProfile.BlockedUser;
        private const string Title = "title";
        private const string Subject = "subject";
        private const string Message = "message";

        private NotificationTemplate _template;
        private NotificationTemplateDto _templateDto;
        private EditNotificationTemplateViewModel _viewModelTemplate;

        [OneTimeSetUp]
        protected override void Initialize()
        {
            base.Initialize();

            _template = new NotificationTemplate
            {
                Id = Id,
                Title = Title,
                Subject = Subject,
                Message = Message,
            };

            _viewModelTemplate = new EditNotificationTemplateViewModel
            {
                Id = Id,
                Subject = Subject,
                Message = Message,
            };

            _templateDto = new NotificationTemplateDto
            {
                Id = Id,
                Title = Title,
                Subject = Subject,
                Message = Message,
            };
        }

        [Test]
        public void MapperProfile_IsValidConfiguration()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void MapperProfile_FromTemplate_To_TemplateDto_MapIsCorrect()
        {
            var templateDto = Mapper.Map<NotificationTemplateDto>(_template);

            Assert.AreEqual(_template.Id, templateDto.Id);
            Assert.AreEqual(_template.Title, templateDto.Title);
            Assert.AreEqual(_template.Subject, templateDto.Subject);
            Assert.AreEqual(_template.Message, templateDto.Message);
        }

        [Test]
        public void MapperProfile_FromEditViewModelTemplate_To_TemplateDto_MapIsCorrect()
        {
            var templateDto = Mapper.Map<NotificationTemplateDto>(_viewModelTemplate);

            Assert.AreEqual(_viewModelTemplate.Id, templateDto.Id);
            Assert.AreEqual(_viewModelTemplate.Subject, templateDto.Subject);
            Assert.AreEqual(_viewModelTemplate.Message, templateDto.Message);
        }

        [Test]
        public void MapperProfile_FromTemplateDto_To_Template_MapIsCorrect()
        {
            var template = Mapper.Map<NotificationTemplate>(_templateDto);

            Assert.AreEqual(_templateDto.Id, template.Id);
            Assert.IsNull(template.Title);
            Assert.AreEqual(_templateDto.Subject, template.Subject);
            Assert.AreEqual(_templateDto.Message, template.Message);
        }

        [Test]
        public void MapperProfile_FromTemplateDto_To_Template_TitleIsIgnored()
        {
            const string expectedTitle = "expectedTitle";
            const string dtoTitle = "dtoTitle";

            _templateDto.Title = dtoTitle;
            _template.Title = expectedTitle;

            Mapper.Map(_templateDto, _template);

            Assert.AreEqual(expectedTitle, _template.Title);
        }
    }
}
