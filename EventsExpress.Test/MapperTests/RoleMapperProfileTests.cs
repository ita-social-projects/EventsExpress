using System;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class RoleMapperProfileTests : MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected override void Initialize()
        {
            base.Initialize();
            Mapper.Initialize(src =>
            {
                src.AddProfile<RoleMapperProfile>();
            });
        }

        [Test]
        public void RoleMapperProfile_Should_HaveValidConfig()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
