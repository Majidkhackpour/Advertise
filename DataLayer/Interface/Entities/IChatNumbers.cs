using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
    public interface IChatNumbers : IHasGuid
    {
        string Number { get; set; }
        AdvertiseType Type { get; set; }
    }
}
