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
using System.Windows.Shapes;
using PizzaShop_Course.ViewModel.Validation;

namespace PizzaShop_Course.View.Customer
{
    public partial class UserCreateView : Window
    {
        public UserCreateView()
        {
            InitializeComponent();
        }
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (EmailValidation.IsValidEmail(textBox.Text))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;
        }
        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length == 0 && !e.Changes.Any(change => change.AddedLength == 1 && change.RemovedLength == 0 && change.Offset == 0 && e.Source == textBox))
            {
                textBox.Text = "+380";
                textBox.CaretIndex = 4;
            }
            else if (textBox.Text.Length == 3 && !textBox.Text.StartsWith("+380"))
            {
                textBox.Text = "+380";
                textBox.CaretIndex = 4;
            }

            if (PhoneNumberValidation.IsValidUkrainianPhoneNumber(textBox.Text))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;
        }

    }
}
