using System;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    using System.Collections.Generic;
    using System.Security.Claims;

    [TestFixture]
    internal class TokenServiceTests : TestInitializer
    {
        private Mock<IJwtSigningEncodingKey> _mockSigningEncodingKey;
        private Mock<IOptions<JwtOptionsModel>> _mockJwtOptions;
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        private TokenService _service;
        private List<Claim> _claims;
        private Account _existingAccount;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            _mockJwtOptions = new Mock<IOptions<JwtOptionsModel>>();
            _mockSigningEncodingKey = new Mock<IJwtSigningEncodingKey>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _service = new TokenService(
                Context,
                MockMapper.Object,
                _mockJwtOptions.Object,
                _mockSigningEncodingKey.Object,
                _httpContextAccessor.Object);

            _existingAccount = new Account
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AccountRoles = new[] { new AccountRole { RoleId = Db.Enums.Role.User } },
            };

            _claims = new List<Claim> { new Claim(ClaimTypes.Name, $"{_existingAccount.UserId}") };

            Context.Accounts.Add(_existingAccount);
            Context.SaveChanges();
        }
    }
}
