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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        public Product product;
        public Product oldProduct;

        public List<Supplier> suppliers;
        public List<Category> categories;
        public List<Unit> units;
        public AddProductPage(Product _product)
        {
            InitializeComponent();
            suppliers = DataAccess.GetSuppliers();
            categories = DataAccess.GetCategoryes();
            units = DataAccess.GetUnits();

            if(_product.Name == null)
            {
                product = new Product();
                oldProduct = new Product();
            }
            else
                product = _product;

            
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var stringBuilder = new StringBuilder();
            try
            {
                if (product.MaxDiscount < 0)
                    stringBuilder.AppendLine("Скидка не может быть < 0");
                if (product.MaxDiscount < 0)
                    stringBuilder.AppendLine("максимальная скидка не может быть < 0");
                if (product.MaxDiscount > 100)
                    stringBuilder.AppendLine("Скидка не может быть < 100");
                if (product.MaxDiscount > 100)
                    stringBuilder.AppendLine("максимальная скидка не может быть > 100");
                if (string.IsNullOrEmpty(product.Name))
                    stringBuilder.AppendLine("Заполните имя");

                if (stringBuilder.Length > 0)
                {
                    MessageBox.Show(stringBuilder.ToString());
                    return;
                }


                DataAccess.SaveProduct(product);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка \n" + stringBuilder.ToString());
            }
        }
    }
}
