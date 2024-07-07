using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Security.Policy;
using System.Net.Http;

namespace ConsoleApp2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                Task.Run(() =>
                {
                    Console.WriteLine("Press 'c' + 'Enter' ");
                    if (Console.ReadKey().KeyChar == 'c')
                    {
                        cts.Cancel();
                    }
                });

                DownloadHtml();
            }

            Console.ReadLine();
        }

        public static void DownloadHtml()
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                List<string> urls = new List<string>
                {
                    "https://chrome.com",
                    "https://youtube.com",
                    "https://X.com"
                };

                HttpClient httpClient = new HttpClient();

                List<Task<string>> downloadTasks = new List<Task<string>>();

                foreach (var url in urls)
                {
                    downloadTasks.Add(LoadUrlAsync(httpClient, url, cts.Token));
                }

                Task.WhenAll(downloadTasks);
            }
    }

        static async Task<string> LoadUrlAsync(HttpClient httpClient, string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

    }
}
