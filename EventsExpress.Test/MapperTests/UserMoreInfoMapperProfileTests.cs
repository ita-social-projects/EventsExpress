using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests;

[TestFixture]
internal class UserMoreInfoMapperProfileTests : MapperTestInitializer<UserMoreInfoMapperProfile>
{
    [SetUp]
    protected override void Initialize()
    {
        base.Initialize();
    }

    [Test]
    public void UserMoreInfoMapperProfile_Should_HaveValidConfig()
    {
        Configuration.AssertConfigurationIsValid();
    }
}
