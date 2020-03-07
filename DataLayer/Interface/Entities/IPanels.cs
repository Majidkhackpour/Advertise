namespace DataLayer.Interface.Entities
{
   public interface IPanels:IHasGuid
    {
        string Name { get; set; }
        string API { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
