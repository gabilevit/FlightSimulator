using Dal.Simulators;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrivalSimulator arrival = new ArrivalSimulator();
            arrival.Start();
            Console.ReadLine();
        }
    }
}
