using EventsExpress.Core.DTOs;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class EntityNameMapperProfileTests : MapperTestInitializer<EntityNameMapperProfile>
    {
        private EntityNamesDto _firstEntityNamesDto;

        private EntityNamesDto GetEntityNamesDto()
        {
            return new EntityNamesDto { EntityName = "Event" };
        }

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void EntityNameMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void EntityNameMapperProfile_EntityNamesDtoToEntityNamesViewModel()
        {
            _firstEntityNamesDto = GetEntityNamesDto();
            var resEven = Mapper.Map<EntityNamesDto, EntityNamesViewModel>(_firstEntityNamesDto);

            Assert.That(resEven.EntityName, Is.EqualTo(_firstEntityNamesDto.EntityName));
        }
    }
}
