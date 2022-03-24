using System;
using System.Diagnostics.CodeAnalysis;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels;

[ExcludeFromCodeCoverage]
public class RegisterCompleteViewModel
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

    public string Phone { get; set; }
}
