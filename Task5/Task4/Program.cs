using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer pc = new ProducerConsumer();
            Thread ProducerThread = new Thread(new ThreadStart(pc.ProducerThread));
            Thread ConsumerThread = new Thread(new ThreadStart(pc.ConsumerThread));

            ProducerThread.Start();
            ConsumerThread.Start();

            Console.ReadLine();
        }
    }

    class ProducerConsumer
    {
        private static Queue<string> ProductBuffer = new Queue<string>(5);

        public void ProducerThread()
        {
            while (true)
            {
                lock (ProductBuffer)
                {
                    Console.WriteLine(5 - ProductBuffer.Count);
                    for (int i = 0; i < 5 - ProductBuffer.Count; i++)
                    {
                        ProductBuffer.Enqueue("Product");
                        Console.WriteLine("Producer har produceret: " + ProductBuffer.Count);
                    }
                    Monitor.PulseAll(ProductBuffer);
                    Thread.Sleep(200);
                }
            }
        }
        public void ConsumerThread()
        {
            while (true)
            {
                lock (ProductBuffer)
                {
                    if (ProductBuffer.Count == 0)
                    {
                        Monitor.Wait(ProductBuffer);
                    }

                    Console.WriteLine("Consumer har consumeret: " + ProductBuffer.Count);
                    ProductBuffer.Dequeue();
                }
            }
        }
    }
}
