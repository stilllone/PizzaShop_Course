using PizzaShop_Course.View.Notification;
using System.Windows;
using System.Windows.Controls;

namespace PizzaShop_Course.View
{
    /// <summary>
    /// Interaction logic for AutorazationView.xaml
    /// </summary>
    public partial class AuthorizationView : UserControl
    {
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChoosePermision ch = new ChoosePermision();
            ch.Show();
        }
    }
}
