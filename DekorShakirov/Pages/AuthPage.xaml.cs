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
using DekorShakirov.DB;

namespace DekorShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        MainWindow MW;
        public AuthPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MW = mainWindow;
            MW.tbTitle.Text = "Авторизация";
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            var login = tbxLogin.Text;
            var password = pbxPassword.Password.ToString();
            var user = DataAccess.GetUser(login, password);
            if (user != null)
            {
                NavigationService.Navigate(new Pages.ProductsPage(MW));
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage(MW));
        }
    }
}
