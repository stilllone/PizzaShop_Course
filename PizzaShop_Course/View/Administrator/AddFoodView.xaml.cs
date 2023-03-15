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

namespace PizzaShop_Course.View.Administrator
{
    /// <summary>
    /// Interaction logic for AddFoodView.xaml
    /// </summary>
    public partial class AddFoodView : UserControl
    {
        public AddFoodView()
        {
            InitializeComponent();
        }
        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != ',')
            {
                e.Handled = true;
            }

            if (e.Text[0] == ',' && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
            }
        }
    }
}
