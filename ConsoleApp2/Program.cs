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

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                var user1 = new User
                {
                    Name = "John",
                    Settings = new UserSettings { Theme = "Dark", ReceiveEmails = true }
                };
                var user2 = new User
                {
                    Name = "Alice",
                    Settings = new UserSettings { Theme = "Light", ReceiveEmails = false }
                };
                var user3 = new User
                {
                    Name = "Bob",
                    Settings = new UserSettings { Theme = "Dark", ReceiveEmails = true }
                };

                context.Users.AddRange(user1, user2, user3);
                context.SaveChanges();
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public UserSettings Settings { get; set; }
        }

        public class UserSettings
        {
            public int Id { get; set; }

            public string Theme { get; set; }
            public bool ReceiveEmails { get; set; }

            public int UserId { get; set; }
            public User User { get; set; }
        }
    }
