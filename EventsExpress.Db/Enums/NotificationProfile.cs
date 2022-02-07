namespace EventsExpress.Db.Enums
{
    public enum NotificationProfile
    {
        BlockedUser = 1,
        CreateEventVerification,
        EventCreated,
        EventStatusCanceled,
        EventStatusBlocked,
        EventStatusActivated,
        ParticipationApproved,
        ParticipationDenied,
        RegisterVerification,
        UnblockedUser,
        OwnEventChanged,
        JoinedEventChanged,
    }
}
