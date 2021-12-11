using System.Reflection;
using EventsExpress.Db.EF;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace EventsExpress.Core.GraphQL.Extensions
{
    public class UseApplicationDbContext : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }
    }
}
