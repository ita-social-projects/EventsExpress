using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels;

[ExcludeFromCodeCoverage]
public class UserMoreInfoCreateViewModel
{
    public ParentStatus? ParentStatus { get; set; }

    public IEnumerable<EventTypes> EventTypes { get; set; }

    public RelationShipStatus? RelationshipStatus { get; set; }

    public TheTypeOfLeisure? LeisureType { get; set; }

    public IEnumerable<InterestReasons> ReasonsForUsingTheSite { get; set; }

    public string AdditionalInfo { get; set; }
}
