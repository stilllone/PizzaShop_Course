using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel.Administrator
{
    public class UserViewModel : PropertyBase
    {
        private UserDBConnection userDBConnection;
        private UserModel user;
        public int Id
        {
            get { return user.Id; }
            set
            {
                if (user.Id != value)
                {
                    user.Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public bool ChangeRoots
        {
            get { return user.ChangeRoots; }
            set
            {
                if (user.ChangeRoots != value)
                {
                    user.ChangeRoots = value;
                    OnPropertyChanged(nameof(ChangeRoots));
                }
            }
        }

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                if (user.FirstName != value)
                {
                    user.FirstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                if (user.LastName != value)
                {
                    user.LastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public byte[] Photo
        {
            get { return user.PhotoPath; }
            set
            {
                if (user.PhotoPath != value)
                {
                    user.PhotoPath = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }

        public string Login
        {
            get { return user.Login; }
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                if (user.Password != value)
                {
                    user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Email
        {
            get { return user.Email; }
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public UserViewModel()
        {
            this.userDBConnection = new UserDBConnection();
            user = new UserModel();
            SaveCommand = new RelayCommand(SaveUser);
            UpdateCommand = new RelayCommand(UpdateUser);
            DeleteCommand = new RelayCommand(DeleteUser);
        }

        private void SaveUser(object parameter)
        {
            userDBConnection.CreateUser(user);
        }

        private void UpdateUser(object parameter)
        {
            userDBConnection.UpdateUser(user);
        }

        private void DeleteUser(object parameter)
        {
            userDBConnection.DeleteUser(user.Id);
        }

    }
}
