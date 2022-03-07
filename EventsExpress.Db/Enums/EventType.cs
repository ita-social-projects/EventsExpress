using System;

namespace EventsExpress.Db.Enums;

[Flags]
public enum EventType : byte
{
    Online = 1,
    Offline = 2,
    Free = 4,
    Paid = 8,
    NearMe = 16,
    AnyDistance = 32,
}
