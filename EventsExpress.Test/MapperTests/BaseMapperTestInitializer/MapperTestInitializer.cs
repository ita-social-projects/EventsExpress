using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests.BaseMapperTestInitializer
{
    public abstract class MapperTestInitializer
    {
        [OneTimeSetUp]
        [Obsolete]
        protected virtual void Initialize()
        {
            Mapper.Reset();
        }
    }
}
