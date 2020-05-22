using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlIntroduction.xaml
    /// </summary>
    public partial class ctrlIntroduction : UserControl
    {
        public string Introduction = "";
        private int loginstate;
        private SQSAdminServiceClient client;
        public ctrlIntroduction(int pstateid)
        {
            loginstate = pstateid;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            //XmlNodeList nodeList = doc.SelectNodes("connectionStrings/StudioMIntroduction");
            //foreach (XmlNode node in nodeList)
            //{
            //    Introduction = @"" + node.SelectSingleNode("Text").InnerText;
            //}
            //this.txtIntro.Text = Introduction;
            //showColumnChart();
        }

        private void showColumnChart()
        {

            //List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            //DataSet ds = client.SQSAdmin_StudioM_GetProductByCategoryCount(loginstate);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    valueList.Add(new KeyValuePair<string, int>(dr["productcategorydesc"].ToString(), int.Parse(dr["number"].ToString())));
            //}
            ////Setting data for column chart
            ////columnChart.DataContext = valueList;
            //pieChart.DataContext = valueList;
        }

    }
}
