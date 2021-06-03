namespace EventsExpress.Test.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EventsExpress.Controllers;
    using EventsExpress.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    internal class FrontConfigControllerTest
    {
        private FrontConfigsController _controller;
        private FrontConfigsViewModel _model;

        [SetUp]
        protected void Initialize()
        {
            _model = new FrontConfigsViewModel()
            {
                FacebookClientId = "FBid",
                GoogleClientId = "GLid",
                TwitterCallbackUrl = "/Twitter",
                TwitterConsumerKey = "y2wdt",
                TwitterConsumerSecret = "y5wdt",
                TwitterLoginEnabled = false,
            };
            IOptions<FrontConfigsViewModel> options = Options.Create(_model);
            _controller = new FrontConfigsController(options);
        }

        [Test]
        public void GetConfigs_OK()
        {
            var expected = _controller.GetConfigs();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
