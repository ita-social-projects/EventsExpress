using System;
using System.Net;
using EventsExpress.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ExtensionMethods
{
    public static class CookieExtension
    {
        public static void SetTokenCookie(this HttpContext context, AuthenticateResponseModel model)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
                Secure = true,
            };

            context.Response.Cookies.Delete("refreshToken");
            context.Response.Cookies.Append("refreshToken", model.RefreshToken, cookieOptions);
        }
    }
}
