using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Users
    {
        private List<User> onlineUsers;

        private List<User> offlineUsers;

        public Users()
        {
            onlineUsers = new List<User>();

            offlineUsers = new List<User>();
        }
        
        public List<User> getOnlineUsers()
        {
            return onlineUsers;
        }

        public List<User> getOfflineUsers()
        {
            return onlineUsers;
        }

        public void addOnlineUser(User user)
        {
            onlineUsers.Add(user);
        }

        public void addOfflineUser(User user)
        {
            onlineUsers.Add(user);
        }

        public void removeOnlineUser(User user)
        {
            onlineUsers.Remove(user);
        }

        public void removeOfflineUser(User user)
        {
            offlineUsers.Remove(user);
        }

        public bool UserLogin(User user)
        {
            return onlineUsers.Any(x => x.password == user.password && x.userName == user.userName);
        }

        public void UserRegister(User user)
        {
            offlineUsers.Add(user);
        }
    }
}
