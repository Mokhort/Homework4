using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    class Program
    {
        static bool flag = true;
        static Context context = new Context();
        static ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
        static async Task Main(string[] args)
        {
            context.Database.EnsureCreated();
            Console.WriteLine("Enter the num: 1 - First request, 2 - Second request, 3 - Exit");
            Task input_request = input_req();
            Task send_request = send_req();
            await input_request;
            await send_request;
            context.Dispose();
        }
        static async Task input_req()
        {
            while (flag)
            {
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "1":
                        queue.Enqueue(1);
                        break;
                    case "2":
                        queue.Enqueue(2);
                        break;
                    case "3":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Enter valid num!");
                        break;
                }
            }
        }
        static async Task send_req()
        {

            while (flag)
            {
                int query;

                if (!queue.TryDequeue(out query))
                {
                    Console.WriteLine("not dequeue");
                }
                else if (query == 1)
                {
                    Console.WriteLine("1qe");
                    Console.WriteLine(query);
                    await Task.Delay(300);
                    Console.WriteLine(query);
                    IQueryable<Passanger> passanger = from ps in context.Passanger select ps;
                    List<Passanger> list_passanger = passanger.ToList();
                    foreach (Passanger passangers in list_passanger)
                        Console.WriteLine($"Name - {passangers.Name} Price - {passangers.LastName} Email - {passangers.Email} ");

                }
                else if (query == 2)
                {
                    Console.WriteLine("2qe");
                    await Task.Delay(300);
                    Console.WriteLine(query);
                    IQueryable<Flight> flight = from ps in context.Flight select ps;
                    List<Flight> list_flight = flight.ToList();
                    foreach (Flight flights in list_flight)
                        Console.WriteLine($"Name - {flights.Name} Price - {flights.Cost}");

                }
            }

        }
    }
}