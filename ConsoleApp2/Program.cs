using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName= "notepad.exe";
            process.Exited += Fineshed;
            process.Start();
            process.Close();
        }

        public static void Fineshed(object sender, EventArgs e)
        {
            Console.WriteLine("Process done");
        }
    }
}
