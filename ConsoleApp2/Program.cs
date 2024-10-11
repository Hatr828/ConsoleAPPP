using Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {

            var authService = new AuthService();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. E");
                Console.WriteLine("2. R");
                Console.WriteLine("3. X");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("P: ");
                        var loginUsername = Console.ReadLine();
                        Console.Write("PP: ");
                        var loginPassword = Console.ReadLine();

                        if (authService.Login(loginUsername, loginPassword))
                        {
                            ShowMainMenu();
                        }
                        break;

                    case "2":
                        Console.Write("P: ");
                        var registerUsername = Console.ReadLine();
                        Console.Write("PP: ");
                        var registerPassword = Console.ReadLine();

                        authService.Register(registerUsername, registerPassword);
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("error");
                        break;
                }
            }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }

    public class AuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService()
        {
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated(); 
        }

        public void Register(string username, string password)
        {
            if (_dbContext.Users.Any(u => u.Username == username))
            {
                return;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public bool Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("User not find");
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
