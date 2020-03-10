namespace DataLayer.Enums
{
    public class SMSAction
    {
        private SMSAction(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
        public static SMSAction Send => new SMSAction("Send");
        public static SMSAction Report => new SMSAction("Report");
        public static SMSAction Credit => new SMSAction("Credit");
        public static SMSAction Received => new SMSAction("Received");
        public static SMSAction Status => new SMSAction("Status");
    }
}
