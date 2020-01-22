namespace AdvertiseApp.Classes
{
   public class IP_Store
    {
        private IP_Store(string value) { Value = value; }

        public string Value { get; set; }

        public static IP_Store IP_Mokhaberat =>
            new IP_Store("192.168.1.1");
    }
}
