using System;
using System.Collections;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Test.ServiceTests.TestClasses.Comparers;

internal class UserDtoComparer : IComparer
{
    public int Compare(object x, object y)
    {
        return x is UserDto left && y is UserDto right
            ? left.Id.CompareTo(right.Id)
            : throw new Exception("Params types are wrong.");
    }
}
