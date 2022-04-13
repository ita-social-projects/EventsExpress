using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs;

[ExcludeFromCodeCoverage]
public class UserMoreInfoDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ParentStatus? ParentStatus { get; set; }

    public IEnumerable<EventTypes> EventTypes { get; set; }

    public RelationShipStatus? RelationShipStatus { get; set; }

    public TheTypeOfLeisure? TheTypeOfLeisure { get; set; }

    public IEnumerable<InterestReasons> ReasonsForUsingTheSite { get; set; }

    public string AdditionalInfo { get; set; }
}
