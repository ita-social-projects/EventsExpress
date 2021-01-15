using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class CategoryMapperProfileTests : MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected override void Initialize()
        {
            base.Initialize();
            Mapper.Initialize(src =>
            {
                src.AddProfile<CategoryMapperProfile>();
            });
        }

        [Test]
        public void CategoryMapperProfile_Should_HaveValidConfig()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
