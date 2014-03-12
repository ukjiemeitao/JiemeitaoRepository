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
using UKjiemeitao.DataObjects;
using UKjiemeitao.WPFUI.TBWCFService;

namespace UKjiemeitao.WPFUI
{
    /// <summary>
    /// Interaction logic for ucAddNewProduct.xaml
    /// </summary>
    public partial class ucAddNewProduct : UserControl
    {
        public ucAddNewProduct()
        {
            InitializeComponent();
        }

        private void btn_UploadProduct_Click_1(object sender, RoutedEventArgs e)
        {
            using (TBProductUploadServiceClient c = new TBProductUploadServiceClient())
            {
                c.UploadProduct();
                MessageBox.Show("初始化淘宝分类数据成功！");
            }
        }
    }
}
