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
using UKjiemeitao.WPFUI.TBWCFService;
using UKjiemeitao.WPFUI.SSWCFService;

namespace UKjiemeitao.WPFUI
{
    /// <summary>
    /// Interaction logic for ucSystemSet.xaml
    /// </summary>
    public partial class ucSystemSet : UserControl
    {
        public ucSystemSet()
        {
            InitializeComponent();
        }

        private void btn_itemcat_Click_1(object sender, RoutedEventArgs e)
        {
            using (TBProductUploadServiceClient c = new TBProductUploadServiceClient())
            {
                c.InitializationItemCat();
                MessageBox.Show("初始化淘宝分类数据成功！");
            }
        }

        private void btn_itemprop_Click_1(object sender, RoutedEventArgs e)
        {
            using (TBProductUploadServiceClient c = new TBProductUploadServiceClient())
            {
                c.InitializationItemProp();
                MessageBox.Show("初始化淘宝属性数据成功！");
            }
        }

        private void btn_propvalue_Click_1(object sender, RoutedEventArgs e)
        {
            using (TBProductUploadServiceClient c = new TBProductUploadServiceClient())
            {
                c.InitializationPropValue();
                MessageBox.Show("初始化淘宝属性值数据成功！");
            }
        }

        private void btn_downloadssretailers_Click(object sender, RoutedEventArgs e)
        {
            using (ShopStyleServiceClient c = new ShopStyleServiceClient())
            {
                c.DownloadRetailers();
            }

        }
    }
}
