
using FlightSimulatorApp.ViewModel;
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

namespace FlightSimulatorApp.View
{
    /// <summary>
    ///
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    { 
        ClientVm clientVm;

        public UserControl1()
        {
            InitializeComponent();
            clientVm = new ClientVm();
            DataContext = clientVm;

        }
        private void UploadCsv(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = "csv";
            ofd.Filter = "CSV Files|*.csv";
            var result = ofd.ShowDialog();
            if (result == true)
            {
                clientVm.PathCsv=ofd.FileName;
                clientVm.client.readCsv();
            }
            
            clientVm.client.readCsv();
        }

        private void UploadXml(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = "xml";
            ofd.Filter = "XML Files|*.xml";
            Console.WriteLine(clientVm.PathXml+"hello");
            var result = ofd.ShowDialog();
            if (result == true)
            {
                clientVm.PathXml = ofd.FileName;
                clientVm.client.readXml();
            }
        }

        private void connectFG(object sender, RoutedEventArgs e)
        {
            clientVm.connect();
            //clientVm.Data.print();
        }
    }
}
