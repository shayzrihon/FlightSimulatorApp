using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class ClientVm : INotifyPropertyChanged
    {
        public ClientTcp client;
        string pathCsv;
        string pathXml;
        public event PropertyChangedEventHandler PropertyChanged;

        public ClientVm()
        {
            client = ClientTcp.getInstance();
            client.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public DataList Data
        {
            get { return client.Data; }
        }
        public string PathCsv
        {
            get
            {
                return this.client.PathCsv;
            }
            set
            {
                this.client.PathCsv = value;
                NotifyPropertyChanged("PathCsv");
                Console.WriteLine(PathCsv);
            }
        }
        public string PathXml
        {
            get
            {
                return this.client.PathXml;
            }
            set
            {
                this.client.PathXml = value;
                NotifyPropertyChanged("PathXml");
            }
        }

        public void connect()
        {
            client.connect();
        }
    }
}
