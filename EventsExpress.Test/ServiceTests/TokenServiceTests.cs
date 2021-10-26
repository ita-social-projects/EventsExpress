using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class TokenServiceTests : TestInitializer
    {
        private Mock<IJwtSigningEncodingKey> _mockSigningEncodingKey;
        private Mock<IOptions<JwtOptionsModel>> _mockJwtOptions;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<IIpProviderService> _iIpProviderService;

        private TokenService _service;
        private List<Claim> _claims;
        private Account _existingAccount;
        private User _existingUser;
        private string _token;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            _mockJwtOptions = new Mock<IOptions<JwtOptionsModel>>();
            _mockSigningEncodingKey = new Mock<IJwtSigningEncodingKey>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _iIpProviderService = new Mock<IIpProviderService>();

            _service = new TokenService(
                Context,
                MockMapper.Object,
                _mockJwtOptions.Object,
                _mockSigningEncodingKey.Object,
                _httpContextAccessor.Object,
                _iIpProviderService.Object);

            _token = Guid.NewGuid().ToString();

            _existingUser = new User
            {
                Id = Guid.NewGuid(),
                Account = _existingAccount,
            };

            _existingAccount = new Account
            {
                Id = Guid.NewGuid(),
                UserId = _existingUser.Id,
                AccountRoles = new[] { new AccountRole { RoleId = Db.Enums.Role.User } },
                RefreshTokens = new List<UserToken> { new UserToken { Token = _token, Type = TokenType.RefreshToken, Expires = DateTime.Now.AddDays(7), Created = DateTime.Now } },
            };

            _claims = new List<Claim> { new Claim(ClaimTypes.Name, $"{_existingAccount.UserId}") };

            Context.Users.Add(_existingUser);
            Context.Accounts.Add(_existingAccount);
            Context.SaveChanges();
        }

        [Test]
        public void GenerateEmailConfirmToken_DoesNotThrows()
        {
            Assert.DoesNotThrowAsync(async () => await _service.GenerateEmailConfirmationToken(_token, _existingUser.Id));
        }

        [Test]
        public void GenerateRefreshToken_DoesNotThrows()
        {
             Assert.DoesNotThrow(() => _service.GenerateRefreshToken());
        }

        [Test]
        public async Task RefreshToken_Correct()
        {
            // Mock Jwt Options, SetUp
            _mockJwtOptions.Object.Value.LifeTime = 5d;
            var res = await _service.RefreshToken(_token);
            Assert.IsInstanceOf<AuthenticateResponseModel>(res);
        }
    }
}
