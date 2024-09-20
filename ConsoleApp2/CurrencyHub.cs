using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class CurrencyHub : Hub
{
    public async Task SendCurrencyUpdate(string currencyPair, decimal rate)
    {
        await Clients.All.SendAsync("ReceiveCurrencyUpdate", currencyPair, rate);
    }

    private void SandMassages()
    {

        var random = new Random();
        var pairs = new[] { "USD/EUR", "GBP/EUR", "USD/GBP" };

        new Timer(async _ =>
        {
            var pair = pairs[random.Next(pairs.Length)];
            var rate = Math.Round((decimal)(random.NextDouble() * 10), 4);
            await SendCurrencyUpdate("ReceiveCurrencyUpdate", pair, rate);
        }, null, 0, 5000);

    }
}
