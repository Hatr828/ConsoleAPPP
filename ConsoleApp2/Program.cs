using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = Path.Combine("MyFile.txt");

        string text = "khjg";
        File.WriteAllText(filePath, text);

        string readText = File.ReadAllText(filePath);
        Console.WriteLine(readText);

        Console.ReadLine();
    }
}
