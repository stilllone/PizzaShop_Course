using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class BasketViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PizzasModel> items = new ObservableCollection<PizzasModel>();
        public ObservableCollection<PizzasModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand AddItemCommand { get; }

        public BasketViewModel()
        {
            AddItemCommand = new RelayCommand(AddItem);
        }

        private void AddItem(object parameter)//abstract item
        {
            if (parameter is PizzasModel item)
            {
                Items.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
