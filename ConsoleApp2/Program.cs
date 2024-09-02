using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

 class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string apiKey;

    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        apiKey = config["TMDbApiKey"];

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("error");
            return;
        }

        Console.WriteLine("Film name");
        string movieTitle = Console.ReadLine();

        await GetMovieInfoAsync(movieTitle);
    }

    private static async Task GetMovieInfoAsync(string movieTitle)
    {
        try
        {
            string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(movieTitle)}";
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject movieData = JObject.Parse(responseBody);

            if (movieData["results"] != null && movieData["results"].HasValues)
            {
                var firstMovie = movieData["results"][0];
                Console.WriteLine($"Н: {firstMovie["title"]}");
                Console.WriteLine($"О: {firstMovie["overview"]}");
                Console.WriteLine($"Д: {firstMovie["release_date"]}");
            }
            else
            {
                Console.WriteLine("error");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"error");
        }
    }
}