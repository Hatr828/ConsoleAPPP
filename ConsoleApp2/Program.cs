using System;
using System.IO;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapHub<CurrencyHub>("/currencyHub");

        app.Run();
    }
}
