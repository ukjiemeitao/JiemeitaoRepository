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

namespace UKjiemeitao.WPFUI
{
    /// <summary>
    /// Interaction logic for MainPageWindow.xaml
    /// </summary>
    public partial class MainPageWindow : Window
    {
        public MainPageWindow()
        {
            InitializeComponent();
        }

        private void menu_SystemSet_Click_1(object sender, RoutedEventArgs e)
        {
            ucSystemSet sysset=new ucSystemSet();
            this.spContent.Children.Clear();
            this.spContent.Children.Add(sysset);
        }

        private void menu_ProductConvert_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void menu_ProductUpload_Click_1(object sender, RoutedEventArgs e)
        {
            ucAddNewProduct uploadproduct = new ucAddNewProduct();
            this.spContent.Children.Clear();
            this.spContent.Children.Add(uploadproduct);
        }

        private void munu_InDataBase_Click_1(object sender, RoutedEventArgs e)
        {
            ucInDatabase indatabase = new ucInDatabase();
            this.spContent.Children.Clear();
            this.spContent.Children.Add(indatabase);
        }
    }
}
