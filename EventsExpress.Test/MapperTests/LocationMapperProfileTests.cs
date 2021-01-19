using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class LocationMapperProfileTests : MapperTestInitializer<LocationMapperProfile>
    {
        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void LocationMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }
    }
}
