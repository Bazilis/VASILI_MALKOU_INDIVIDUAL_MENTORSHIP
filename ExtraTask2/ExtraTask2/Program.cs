using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ExtraTask2
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Read and count speed comparer";
            Console.ForegroundColor = ConsoleColor.Green;

            var timer2 = Stopwatch.StartNew();
            var task1 = Task.Factory.StartNew(() => FileReaderCounterAsync.ReadAndCountFile1Async());
            var task2 = Task.Factory.StartNew(() => FileReaderCounterAsync.ReadAndCountFile2Async());
            timer2.Stop();

            Console.WriteLine($"Async method time => {timer2.Elapsed.TotalMilliseconds}");



            var myThread1 = new Thread(FileReaderCounter.ReadAndCountFile1);
            var myThread2 = new Thread(FileReaderCounter.ReadAndCountFile2);

            var timer1 = Stopwatch.StartNew();
            myThread1.Start();
            myThread2.Start();
            timer1.Stop();

            Console.WriteLine($"Multi-threaded method time => {timer1.Elapsed.TotalMilliseconds}");

            Console.ReadLine();
        }
    }
}
