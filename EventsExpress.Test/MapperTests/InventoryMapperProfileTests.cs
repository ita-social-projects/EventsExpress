using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class InventoryMapperProfileTests : MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected override void Initialize()
        {
            base.Initialize();
            Mapper.Initialize(src =>
            {
                src.AddProfile<InventoryMapperProfile>();
            });
        }

        [Test]
        public void InventoryMapperProfile_Should_HaveValidConfig()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
