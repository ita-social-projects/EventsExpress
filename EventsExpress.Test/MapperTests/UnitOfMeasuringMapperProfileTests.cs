using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class UnitOfMeasuringMapperProfileTests : MapperTestInitializer<UnitOfMeasuringMapperProfile>
    {
        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void UnitOfMeasuringMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }
    }
}
