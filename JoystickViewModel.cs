using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    
    class JoystickViewModel : INotifyPropertyChanged
    {
        private ClientTcp client;
        private Double elevator, aileron;
        public event PropertyChangedEventHandler PropertyChanged;
        public JoystickViewModel()
        {
            client = ClientTcp.getInstance();
            elevator = 0.0;
            aileron = 0.0;
            client.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if(e.PropertyName == "Line")
                {
                    Aileron = client.dataModel.getValueAtLine("aileron", client.Line);
                    Elevator = client.dataModel.getValueAtLine("elevator", client.Line);

                }

            };
            
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Double Elevator {
            get { return elevator; }
            set { elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public Double Aileron
        {
            get { return aileron; }
            set { aileron = value;
                Console.WriteLine("aileron changed");
                NotifyPropertyChanged("Aileron");
            }
        }

    }

}
