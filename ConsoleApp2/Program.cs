using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlTypes;

namespace ConsoleApp2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount(1, 10000);
            ATM atm = new ATM(bankAccount);

            atm.DrawMoney(1000);
            atm.DrawMoney(9000);

            Thread.Sleep(500);
            Console.WriteLine(bankAccount.money);


            Console.ReadLine();
        }     
        
    }

    public class BankAccount
    {
        private int id;

        public ulong money;

        private int password;

        public BankAccount(int id, ulong money)
        {
            this.id = id;
            this.money = money;
        }

        public void DrawMoney(ulong amount)
        {
            lock(this)
            {
                if (this.money >= amount)
                {
                    this.money -= amount;

                    Console.WriteLine("Money left: " + this.money);
                }
                else
                {
                    Console.WriteLine("Not enough money");
                }
            }
        }
    }

    public class ATM
    {
        private BankAccount account;

        public ATM(BankAccount account)
        {
            this.account = account;
        }

        public void DrawMoney(ulong amount)
        {
            Task.Run(() => account.DrawMoney(amount));
        }
    }
}
