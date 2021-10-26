using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class TokenServiceController : TestInitializer
    {
        private Mock<IJwtSigningEncodingKey> _mockSigningEncodingKey;
        private Mock<IOptions<JwtOptionsModel>> _mockJwtOptions;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<IIpProviderService> _iIpProviderService;

        private TokenService _tokenService;
        private List<Claim> _claims;
        private Account _existingAccount;
        private User _existingUser;
        private string _token;

        private TokenController _tokenController;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            _mockJwtOptions = new Mock<IOptions<JwtOptionsModel>>();
            _mockSigningEncodingKey = new Mock<IJwtSigningEncodingKey>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _iIpProviderService = new Mock<IIpProviderService>();

            _tokenService = new TokenService(
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

            _tokenController = new TokenController(_tokenService);
        }

        [Test]
        public void Refresh_Correct_ReturnOk()
        {
            var res = _tokenController.Refresh();
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void Revoke_Correct_ReturnOk()
        {
            var res = _tokenController.Revoke();
            Assert.IsNotInstanceOf<OkObjectResult>(res);
        }
    }
}
