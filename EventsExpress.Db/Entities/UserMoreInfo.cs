﻿using System;
using System.Diagnostics.CodeAnalysis;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities;

[ExcludeFromCodeCoverage]
public class UserMoreInfo : BaseEntity
{
    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public ParentStatus? ParentStatus { get; set; }

    public EventTypes? EventTypes { get; set; }

    public RelationShipStatus? RelationShipStatus { get; set; }

    public TheTypeOfLeisure? TheTypeOfLeisure { get; set; }

    public InterestReasons? ReasonsForUsingTheSite { get; set; }

    public string AdditionalInfo { get; set; }
}
