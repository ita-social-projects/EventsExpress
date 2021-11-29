using System;
using System.Collections.Generic;
using EventsExpress.Core.Exceptions;
using EventsExpress.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace EventsExpress.Test.FilterTests
{
    [TestFixture]
    internal class EventsExpressExceptionFilterTests
    {
        private ActionContext _actionContext;

        [SetUp]
        public void Initialize()
        {
            _actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
        }

        [Test]
        public void OnException_WhenExpressExceptionThrown_StatusCodeShouldBe400()
        {
            var filter = new EventsExpressExceptionFilterAttribute();
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = new EventsExpressException(),
            };
            const int expected = StatusCodes.Status400BadRequest;

            filter.OnException(exceptionContext);
            Assert.IsInstanceOf<ObjectResult>(exceptionContext.Result);
            var result = (ObjectResult) exceptionContext.Result;
            var actual = result.StatusCode;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OnException_WhenExpressExceptionThrown_ResultObjectShouldHaveErrors()
        {
            const string message = "Validation error occurred";
            var filter = new EventsExpressExceptionFilterAttribute();
            var validationErrors = new Dictionary<string, string>
            {
                { "Field", "Validation error" },
            };
            var expectedErrors = new Dictionary<string, Array>
            {
                { "_error", new[] { message } },
                { "Field", new[] { "Validation error" } },
            };
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = new EventsExpressException(message, validationErrors),
            };
            var expected = JsonConvert.SerializeObject(new { Errors = expectedErrors });

            filter.OnException(exceptionContext);
            Assert.IsInstanceOf<ObjectResult>(exceptionContext.Result);
            var result = (ObjectResult) exceptionContext.Result;
            var actual = JsonConvert.SerializeObject(result.Value);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OnException_WhenExceptionThrown_StatusCodeShouldBe500()
        {
            var filter = new EventsExpressExceptionFilterAttribute();
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception(),
            };
            const int expected = StatusCodes.Status500InternalServerError;

            filter.OnException(exceptionContext);
            Assert.IsInstanceOf<ObjectResult>(exceptionContext.Result);
            var result = (ObjectResult) exceptionContext.Result;
            var actual = result.StatusCode;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OnException_WhenExceptionThrown_ResultObjectShouldHaveErrorWithMessage()
        {
            var filter = new EventsExpressExceptionFilterAttribute();
            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception(),
            };
            const string message = "Unhandled exception occurred. Please try again. "
                                    + "If this error persists - contact system administrator.";
            var expectedErrors = new Dictionary<string, Array>
            {
                { "_error", new[] { message } },
            };
            var expected = JsonConvert.SerializeObject(new { Errors = expectedErrors });

            filter.OnException(exceptionContext);
            Assert.IsInstanceOf<ObjectResult>(exceptionContext.Result);
            var result = (ObjectResult) exceptionContext.Result;
            var actual = JsonConvert.SerializeObject(result.Value);

            Assert.AreEqual(expected, actual);
        }
    }
}
