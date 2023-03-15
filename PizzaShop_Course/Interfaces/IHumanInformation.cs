using System.Windows.Controls;

namespace PizzaShop_Course.Interfaces
{
    public interface IHumanInformation : IHumanDataAuthorization
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool ChangeRoots { get; set; }
        byte[] Photo { get; set; }
    }
}
