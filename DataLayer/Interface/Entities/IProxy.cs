using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
    public interface IProxy : IHasGuid
    {
        string Server { get; set; }
        int Port { get; set; }
        string Secret { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        ProxyType Type { get; set; }
    }
}
