namespace EventsExpress.Validation.ValidationMessages
{
    public static class LocationValidationMessage
    {
        public const string TypeMessage = "Field Location Type is required!";
        public const string LatitudeMessage = "Field is required!";
        public const string LongitudeMessage = "Field is required!";

        public static string OnlineMeetingMessage(string inputUrl)
        {
            return $"Link '{inputUrl}' must be a valid URI. eg: http://www.SomeWebSite.com.au";
        }
    }
}
