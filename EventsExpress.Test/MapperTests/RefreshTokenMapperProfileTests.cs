using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class RefreshTokenMapperProfileTests : MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected override void Initialize()
        {
            base.Initialize();
            Mapper.Initialize(src =>
            {
                src.AddProfile<RefreshTokenMapperProfile>();
            });
        }

        [Test]
        public void RefreshTokenMapperProfile_Should_HaveValidConfig()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
