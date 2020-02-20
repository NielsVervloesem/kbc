using PusherClient;



namespace KBCFoodAndGo.Shared.Services
{
    public static class PusherService
    {
        public static Pusher Pusher { get; set; } = InitializeResourcePusher();

        static Pusher InitializeResourcePusher()
        {
            var options = new PusherOptions
            {
                Encrypted = true,
                Cluster = "eu"
            };

            Pusher = new Pusher("3e81de0297856b72df54", options);
            Pusher.Connect();
            return Pusher;
        }
    }
}

