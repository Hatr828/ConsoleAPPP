using Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {

            static void Main()
            {
                UserManagement userManager = new UserManagement();

   
                userManager.AddUser(new User(1, "11") { Settings = new UserSettings { Theme = "Dark", NotificationsEnabled = true } });
                userManager.AddUser(new User(2, "22") { Settings = new UserSettings { Theme = "Light", NotificationsEnabled = false } });
                userManager.AddUser(new User(3, "33") { Settings = new UserSettings { Theme = "Dark", NotificationsEnabled = true } });

                var user = userManager.GetUserById(2);
                if (user != null)
                {
                    Console.WriteLine($"{user.Name}  {user.Settings.Theme}  {user.Settings.NotificationsEnabled}");
                }

                userManager.DeleteUserById(3);

                var deletedUser = userManager.GetUserById(3);
                if (deletedUser == null)
                {
                    Console.WriteLine("S");
                }
            }
        }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public UserSettings Settings { get; set; }

            public User(int id, string name)
            {
                Id = id;
                Name = name;
                Settings = new UserSettings { UserId = id };
            }
        }

        public class UserSettings
        {
            public int UserId { get; set; }
            public string Theme { get; set; }
            public bool NotificationsEnabled { get; set; }
        }

        public class UserManagement
        {
            private List<User> users = new List<User>();

            public void AddUser(User user)
            {
                users.Add(user);
            }

            public User GetUserById(int id)
            {
                return users.FirstOrDefault(u => u.Id == id);
            }

            public void DeleteUserById(int id)
            {
                users.RemoveAll(u => u.Id == id);
            }
        }

    }
