using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EventsExpress.Test.PoliciesTests
{
    public class RoleHanlderTest
    {
        [Test]
        public async Task RoleHandler_ReturnsSucceed_TestAsync()
        {
            // Arrange
            var handler = new RoleHandler();
            var authorizationService = BuildAuthorizationService(services =>
            {
                services.AddSingleton<IAuthorizationHandler>(handler);
                services.AddAuthorization(options =>
                {
                    options.AddPolicy(PolicyNames.AdminPolicyName, policy => policy.Requirements.Add(new RoleRequirement(PolicyNames.AdminRole)));
                });
            });

            // Act
            var allowed = await authorizationService.AuthorizeAsync(new ClaimsPrincipal(), PolicyNames.AdminPolicyName);

            // Assert
            Assert.False(allowed.Succeeded);
        }

        private IAuthorizationService BuildAuthorizationService(Action<IServiceCollection> setupServices = null)
        {
            var services = new ServiceCollection();
            services.AddAuthorization();
            services.AddLogging();
            services.AddOptions();
            setupServices?.Invoke(services);
            return services.BuildServiceProvider().GetRequiredService<IAuthorizationService>();
        }
    }
}
