using System;
using System.Diagnostics.CodeAnalysis;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs;

[ExcludeFromCodeCoverage]
public class RegisterCompleteDto
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

    public string Phone { get; set; }

    public Guid AccountId { get; set; }
}
