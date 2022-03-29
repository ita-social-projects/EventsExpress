using System;

namespace EventsExpress.Db.Enums;

[Flags]
public enum InterestReasons : byte
{
    DevelopASkill = 1,
    MeetPeopleLikeMe = 2,
    BeMoreActive = 4,
}
