using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FlightSimulatorApp.Model
{
    class ClientTcp:INotifyPropertyChanged
    {
        public DataList dataModel;
        string pathCsv;
        string pathXml;
        private TcpClient _client;
        string ip;
        int port;
        private int _line;
        private FlightGearLauncher f;

        private static ClientTcp instance;
        public static ClientTcp getInstance()
        {
            if (instance != null) {
                return instance;
            }
            else
            {
                instance = new ClientTcp();
                return instance;
            }
        }

        private ClientTcp()
        {
            Console.WriteLine("TcpClientFG Constructor ... ");
            _client = new TcpClient();
            dataModel = new DataList();
            ip = "127.0.0.1";
            port = 5400;
            pathCsv = "";
            pathXml = "";
            _line = 0;
        }
        public DataList Data
        {
            get
            {
                return dataModel;
            }
        }
        public string PathCsv
        {
            get { return pathCsv; }
            
            set
            {
                pathCsv = value;
                ConfigurationSettings.AppSettings.Set("csv_input", value);
                NotifyPropertyChanged("PathCsv");
            }
        }
        public string PathXml
        {
            get { return pathXml; }
            set {
                pathXml = value;
                ConfigurationSettings.AppSettings.Set("in_xml", value);
                NotifyPropertyChanged("PathXml");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void HandleCommunication(object obj)
        {

            Console.WriteLine("Client is connected to FlightGear ... ");
            //TcpClient client = (TcpClient)obj;
            //NetworkStream __clientStream = client.GetStream();

            //StreamReader input = new StreamReader(PathCsv);
            //StreamWriter output = new StreamWriter(__clientStream);
            //String line;
            Console.WriteLine("Client is streaming CSV data to FlightGear ... ");
            while ((Line < 200) != null)
            {
                /*
                Console.WriteLine("\n ############################################ Sent To FlightGear ##################################################### \n");
                Console.WriteLine(line);
                Console.WriteLine("\n ##################################################################################################################### \n");
                */

                //output.WriteLine(line);
                //output.Flush();
                sendLine(dataModel.getLine(Line));
                //Console.WriteLine(dataModel.getLine(_line));
                System.Threading.Thread.Sleep(10);
                Line++;
            }

        }

        public void sendLine(string line)
        {
            NetworkStream __clientStream = _client.GetStream();
            line += "\r\n";
            __clientStream.Write(System.Text.Encoding.ASCII.GetBytes(line), 0, line.Length);
        }

        // connect to flightGear simulator
        public void connect()
        {
            if (pathXml!= ""&&pathCsv != "")
            {
                //f = new FlightGearLauncher();
                System.Threading.Thread.Sleep(600);
                _client.Connect(ip, port);
                //sendLine(dataModel.getLine(_line));


                Thread t = new Thread(new ParameterizedThreadStart(HandleCommunication));
                t.Start(_client);
            }
        }
        // read the xml file  
        public void readXml()
        {
            XmlReader reader = XmlReader.Create(PathXml);
            while (reader.Read())
            {
                if (reader.IsStartElement())
                { 
                    switch (reader.Name.ToString())
                    {
                        case "name":
                            dataModel.addName(reader.ReadString());
                            break;
                        case "input":
                            return;
                    }
                }
            }
        }
        // read the csv file
        public void readCsv()
        {
            int size = dataModel.data.Count;
            using (var reader = new StreamReader(PathCsv))
            {
                // 42 lists for 42 rows in csv file
                

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    for (int i=0;i< size;++i)
                    {
                        dataModel.data[i].Item2.Add(Convert.ToDouble(values[size-1-i]));
                    }
                }
                
            }

        }
        public int Line
        {
            get { return _line; }
            set
            {
                _line = value;
                NotifyPropertyChanged("Line");
            }
        }
    }
}
