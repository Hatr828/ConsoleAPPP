using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class DiceGame
{
    private static readonly string apiKey = "none";
    private static readonly string apiUrl = "https://api.random.org/json-rpc/4/invoke";

    static async Task Main(string[] args)
    {
        Console.WriteLine("1. c c");
        Console.WriteLine("2. cc cc");
        string gameMode = Console.ReadLine();

        if (gameMode == "1")
        {
            await PlayHumanVsHuman();
        }
        else if (gameMode == "2")
        {
            await PlayHumanVsComputer();
        }
    }

    private static async Task PlayHumanVsHuman()
    {

        while (true)
        {
            Console.WriteLine("Enter");
            Console.ReadLine();
            int player1Roll = await RollDice();
            Console.WriteLine($"Игрок 1 выбросил: {player1Roll}");

            Console.WriteLine("Enter");
            Console.ReadLine();
            int player2Roll = await RollDice();
            Console.WriteLine($"{player2Roll}");

            DetermineWinner(player1Roll, player2Roll);

            Console.WriteLine("Again (y/n)");
            string playAgain = Console.ReadLine();
            if (playAgain.ToLower() != "y")
                break;
        }
    }

    private static async Task PlayHumanVsComputer()
    {

        while (true)
        {
            Console.WriteLine("Enter");
            Console.ReadLine();
            int humanRoll = await RollDice();
            Console.WriteLine($"{humanRoll}");

            int computerRoll = await RollDice();
            Console.WriteLine($"{computerRoll}");

            DetermineWinner(humanRoll, computerRoll);

            Console.WriteLine("Again (y/n)");
            string playAgain = Console.ReadLine();
            if (playAgain.ToLower() != "y")
                break;
        }
    }

    private static void DetermineWinner(int roll1, int roll2)
    {
        if (roll1 > roll2)
        {
            Console.WriteLine("1 Win");
        }
        else if (roll2 > roll1)
        {
            Console.WriteLine("2 Win");
        }
        else
        {
            Console.WriteLine("NN");
        }
    }

    private static async Task<int> RollDice()
    {
        using (var client = new HttpClient())
        {
            var requestBody = new
            {
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new
                {
                    apiKey = apiKey,
                    n = 1,
                    min = 1,
                    max = 6
                },
                id = 42
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            string responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RandomOrgResponse>(responseString);

            return result.result.random.data[0];
        }
    }
}

public class RandomOrgResponse
{
    public Result result { get; set; }
}

public class Result
{
    public RandomData random { get; set; }
}

public class RandomData
{
    public int[] data { get; set; }
}
