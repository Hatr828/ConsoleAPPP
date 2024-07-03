using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cashier[] cashiers = new Cashier[5];
            Thread[] threads = new Thread[5];

            for (int i = 0; i < cashiers.Length; i++)
            {
                int cashierIndex = i;
                //Не знаю почему, но у меня выводит ошибку если пробую запускать код, но если использовать переменную тогда все норм, хотя разницы не должно быть.

                cashiers[i] = new Cashier();
                threads[i] = new Thread(() => cashiers[i].Work());   //   threads[i] = new Thread(() => cashiers[cashierIndex].Work());  только так все работает

                for (int j = 0; j < 5; j++)
                {
                    cashiers[i].costumers.Enqueue(new Customer());
                }
            }

            for (int i = 0; i < 5; i++)
            {
                threads[i].Start();
            }

            Console.ReadLine();
        }     
    }

    public class Customer
    {
       public int Id;

    }

    public class Cashier
    {
       public Queue<Customer> costumers;
       
       private Random rd = new Random();


        public Cashier()
        {
            costumers = new Queue<Customer>();
        }

        public void Work()
        {
            while (true)
            {
                if (costumers == null || costumers.Count <= 0)
                {
                    Console.WriteLine("Done: " + Thread.CurrentThread);
                    break;
                }
                else
                {
                    Console.WriteLine(" In progress left: " + costumers.Count);
                    Thread.Sleep(rd.Next(1000, 3000));
                    costumers.Dequeue();
                }
            }
        }
    }
}
