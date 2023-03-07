using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaShop_Course.View
{
    /// <summary>
    /// Interaction logic for DrinksView.xaml
    /// </summary>
    public partial class DrinksView : UserControl
    {
        public DrinksView()
        {
            InitializeComponent(); InitializeComponent();
            var products = GetProducts();
            if (products.Count > 0)
                ListViewProducts.ItemsSource = products;
        }

        private List<DrinksModel> GetProducts()
        {
            var products = new List<DrinksModel>();
            products.Add(new DrinksModel());
            return products;
            
        }
    }
}
