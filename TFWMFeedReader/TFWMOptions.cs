namespace TFWMFeedReader
{
    public class TFWMOptions
    {
        public String BaseUri { get; set; } = "http://api.tfwm.org.uk/";
        public String Endpoint { get; set; } = "gtfs/trip_updates";

        public String ApiUser { get; set; }
        public String ApiKey { get; set; }
    }
}
