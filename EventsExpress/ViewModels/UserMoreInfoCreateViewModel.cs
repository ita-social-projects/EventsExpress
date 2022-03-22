using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels;

public class UserMoreInfoCreateViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ParentStatus? ParentStatus { get; set; }

    public IEnumerable<EventType> EventTypes { get; set; }

    public RelationShipStatus? RelationshipStatus { get; set; }

    public TheTypeOfLeisure? LeisureType { get; set; }

    public IEnumerable<ReasonsForUsingTheSite> ReasonsForUsingTheSite { get; set; }

    public string AdditionalInfo { get; set; }
}
