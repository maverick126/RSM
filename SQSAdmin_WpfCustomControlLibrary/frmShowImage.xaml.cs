using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmShowImage.xaml
    /// </summary>
    public partial class frmShowImage : Window
    {
        private StudioMResource.ProductImage image;
        public frmShowImage(StudioMResource.ProductImage p)
        {
            image = new StudioMResource.ProductImage();
            image = p;
            InitializeComponent();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(image.ImageStream);
            bi.EndInit();
            img.Source = bi;
        }


    }
}
