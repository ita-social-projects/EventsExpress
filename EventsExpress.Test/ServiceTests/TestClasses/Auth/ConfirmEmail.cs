using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Auth
{
    internal class ConfirmEmail
    {
        internal static readonly Guid AuthLocalId = Guid.NewGuid();

        internal static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(Guid.NewGuid(), "valid token")
                    .SetName("ConfirmEmail_InvalidAuthId_Throws");
                yield return new TestCaseData(AuthLocalId, "invalid token")
                    .SetName("ConfirmEmail_InvalidToken_Throws");
            }
        }
    }
}
