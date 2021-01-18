using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class UnitOfMeasuringMapperProfileTests : MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected override void Initialize()
        {
            base.Initialize();
            Mapper.Initialize(src =>
            {
                src.AddProfile<UnitOfMeasuringMapperProfile>();
            });
        }

        [Test]
        public void UnitOfMeasuringMapperProfile_Should_HaveValidConfig()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
