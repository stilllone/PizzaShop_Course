using PizzaShop_Course.Interfaces;

namespace PizzaShop_Course.Model
{
    public class UserModel : PropertyBase, IHumanInformation
    {
        private bool changeRoots;
        public bool ChangeRoots
        {
            get => changeRoots;
            set
            {
                changeRoots = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private byte[] photopath;
        public byte[] PhotoPath
        {
            get => photopath;
            set
            {
                photopath = value;
                OnPropertyChanged();
            }
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

       
    }
}
