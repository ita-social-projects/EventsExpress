using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class UserEventInventoryMapperProfileTests : MapperTestInitializer<UserEventInventoryMapperProfile>
    {
        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void UserEventInventoryMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }
    }
}
