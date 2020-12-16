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
        private static Queue<string> ProductBuffer = new Queue<string>(3);

        public void ProducerThread()
        {
            while (true)
            {
                if(ProductBuffer.Count == 3)
                {
                    Console.WriteLine("Producer fik ikke lov til at producere: " + ProductBuffer.Count);
                }
                else if (ProductBuffer.Count == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ProductBuffer.Enqueue("Product");
                        Console.WriteLine("Producer har produceret: " + ProductBuffer.Count);
                    }
                }
                Thread.Sleep(200);
            }
        }
        public void ConsumerThread()
        {
            while (true)
            {
                if (ProductBuffer.Count == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine("Consumer har consumeret " + ProductBuffer.Count);
                        ProductBuffer.Dequeue();
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
