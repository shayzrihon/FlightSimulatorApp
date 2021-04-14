using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class FlightGearLauncher
    {

        public FlightGearLauncher()
        {
            Console.WriteLine("FlightGearLauncher Constructor ... ");

            Thread t = new Thread(Launch);
            t.Start();
        }

        public void Launch()
        {
            Console.WriteLine("Loading FlightGear ...");

            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = ConfigurationSettings.AppSettings.Get("flightgear_exe");
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.StartInfo.Arguments = ConfigurationSettings.AppSettings.Get("flightgear_params");
            ExternalProcess.Start();
            System.Threading.Thread.Sleep(80000);// 20 seconds to load
            Console.WriteLine("FlightGear Loaded ...");
            ExternalProcess.WaitForExit();

        }
    }
}
