using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread tr = new Thread(() => DwnloadTest());
            tr.Start();
        }

        private static void DwnloadTest()
        {
            string url = "https://axiomabio.com/pdf/test.pdf";
            string destinationPath = @"C:/";

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFile(url, destinationPath);
                }
                catch (Exception e)
                {
                    //...
                }
            }
        }
    }
}
