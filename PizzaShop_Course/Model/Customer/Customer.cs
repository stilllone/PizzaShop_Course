using PizzaShop_Course.Interfaces;

namespace PizzaShop_Course.Model.Customer
{
    public class Customer : PropertyBase, IHumanInformation
    {
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
        public byte[] PhotoPath //INSERT INTO xx_BLOB(ID,IMAGE) VALUES(1, LOAD_FILE('E:/Images/xxx.png'));
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

        private double amount;
        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        private bool changeroots;
        public bool ChangeRoots
        {
            get => changeroots;
            set
            {
                changeroots = value;
                OnPropertyChanged();
            }
        }

        private string moneySpend;
        public string MoneySpend
        {
            get => moneySpend;
            set
            {
                moneySpend = value;
                OnPropertyChanged();
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }
    }
}
