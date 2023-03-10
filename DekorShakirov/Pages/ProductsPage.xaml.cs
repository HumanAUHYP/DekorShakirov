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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        MainWindow MW;
        List<Product> Products;
        List<Product> AllProducts;
        const int ITEMONPAGE = 20;
        int pageIndex = 0;
        public ProductsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MW = mainWindow;
            MW.tbTitle.Text = "Таблица продуктов";

            Products = DataAccess.GetProducts();
            AllProducts = Products;
            lvTable.ItemsSource = Products;

            GeneratePages();
            cbDiscount.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFilters();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }

        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (lvTable.SelectedItem != null)
            {
                var product = lvTable.SelectedItem as Product;
                NavigationService.Navigate(new AddProductPage(product));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage(new Product()));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Paginator(object sender, MouseButtonEventArgs e)
        {
            var content = (sender as TextBlock).Text;

            var pagesCount = (int)Math.Ceiling((double)Products.Count / ITEMONPAGE);

            if (content.Contains("<") && pageIndex > 0)
                pageIndex--;
            else if (content.Contains(">") && pageIndex < pagesCount - 1)
                pageIndex++;
            else if (int.TryParse(content, out int pageNum))
                pageIndex = pageNum - 1;

            GeneratePages();
        }

        private void GeneratePages()
        {
            spPages.Children.Clear();

            var pagesCount = (int)Math.Ceiling((double)Products.Count / ITEMONPAGE);

            for (int i = 0; i < pagesCount; i++)
            {
                spPages.Children.Add(new TextBlock
                {
                    Text = (i + 1).ToString()
                });

                spPages.Children[i].PreviewMouseDown += Paginator;
            }
            if (spPages.Children.Count != 0)
            {
                if (pageIndex > spPages.Children.Count)
                    pageIndex = 0;
                (spPages.Children[pageIndex] as TextBlock).TextDecorations = TextDecorations.Underline;
            }


            DisplayClientsInPage();
        }

        private void DisplayClientsInPage()
        {
            var productsInPage = new List<Product>();
            for (int i = pageIndex * ITEMONPAGE; i < (pageIndex + 1) * ITEMONPAGE; i++)
            {
                try
                {
                    productsInPage.Add(Products[i]);
                }
                catch (Exception)
                {
                    break;
                }

            }
            lvTable.ItemsSource = productsInPage;
        }

        private void AllFilters()
        {
            Products = AllProducts;

            var search = tbSearch.Text.ToLower().Trim();

            if (search != "")
            {
                Products = Products.FindAll(a => a.Name.ToLower().Contains(search));
            }
            var sort = cbSort.SelectedItem as TextBlock;
            if (sort == tbCostSort) Products.OrderBy(a => a.Price);
            else if (sort == tbCostDescSort) Products.OrderByDescending(a => a.Price);

            var discount = cbDiscount.SelectedItem as TextBlock;

            if (discount == tb0_10) Products = Products.FindAll(a => a.MaxDiscount < 11);
            else if (discount == tb11_14) Products = Products.FindAll(a => a.MaxDiscount > 10 && a.MaxDiscount < 15);
            else if (discount == tb15) Products = Products.FindAll(a => a.MaxDiscount >= 15);

            GeneratePages();
        }
    }
}
