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

namespace UKjiemeitao.WPFUI
{
    /// <summary>
    /// Interaction logic for ucInDatabase.xaml
    /// </summary>
    public partial class ucInDatabase : UserControl
    {
        public ucInDatabase()
        {
            InitializeComponent();
        }

        private void btn_InDataBase_Click_1(object sender, RoutedEventArgs e)
        {
            using (TBProductUploadServiceClient c = new TBProductUploadServiceClient())
            {
                c.ConvertSSProductToTaoBao();
                MessageBox.Show("初始化淘宝分类数据成功！");
            }
        }
    }
}
