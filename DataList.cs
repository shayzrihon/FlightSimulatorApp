using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class DataList
    {
        public List<Tuple<string, List<double>>> data;
        public DataList()
        {
            this.data = new List<Tuple<string, List<double>>>();
        }

        public void addName(string name)
        {
            data.Add(new Tuple<string, List<double>>(name, new List<double>()));
        }

        public void print()
        {
            foreach(Tuple<string, List<double>> i in data)
            {
                Console.WriteLine(i.Item1);
            }
        }

        public string getLine(int lineInt)
        {
            string line = "";
            for(int i = 0; i<data.Count; i++)
            {
                line += data[i].Item2[lineInt] + ",";
            }
            line.TrimEnd(new char[] { ',' });
            return line;
        }

        public Double getValueAtLine(string name, int row)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if( data[i].Item1 == name)
                {
                    return data[i].Item2[row];
                }
            }
            return 0.0;
        }
    }
}
