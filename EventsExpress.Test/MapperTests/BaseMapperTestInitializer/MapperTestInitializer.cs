using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests.BaseMapperTestInitializer
{
    public abstract class MapperTestInitializer<T>
        where T : Profile, new()
    {
        protected IMapper Mapper { get; set; }

        protected MapperConfiguration Configuration { get; set; }

        protected virtual void Initialize()
        {
            Configuration = new MapperConfiguration(src =>
            {
                src.AddProfile<T>();
            });

            Mapper = Configuration.CreateMapper();
        }
    }
}
