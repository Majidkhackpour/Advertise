namespace AdvertiseApp.Classes
{
   public class CookieStore
    {
        private CookieStore(string value) { Value = value; }

        public string Value { get; set; }

        public static CookieStore NiazmandyHaCookieName =>
            new CookieStore("remember_web_59ba36addc2b2f9401580f014c7f58ea4e30989d");
    }
}
