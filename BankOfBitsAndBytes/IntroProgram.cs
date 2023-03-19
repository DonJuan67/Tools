using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace MultiThread
{
    class Program
    {
        public static int someInt;
        private static object myLock = new object();
        public static Mutex mutex = new Mutex();
        public static Semaphore sem = new Semaphore(2, 2);

        public delegate void delg();



        //static void Main(string[] args)
        //{


        //    //{
        //    //    ThreadStart ts = new ThreadStart(PrintOutOnes);     //Create a thread start given delegate
        //    //    Thread t = new Thread(ts);                          //Create thread with threadstart
        //    //    //t.Priority = ThreadPriority.Highest;
        //    //    t.Start();                                          //Start the thread
        //    //}


        //    //{
        //    //    ThreadStart ts = new ThreadStart(PrintOutZeros);     //Create a thread start given delegate
        //    //    Thread t = new Thread(ts);                          //Create thread with threadstart
        //    //    t.Start();                                          //Start the thread
        //    //}

        //    //for (int i = 0; i < 5; i++)
        //    //{
        //    //    int k = i;
        //    //    ThreadStart ts = new ThreadStart(() => { GoatNoises(k); });     //Create a thread start given delegate
        //    //    Thread t = new Thread(ts);                          //Create thread with threadstart
        //    //    t.Start();                                          //Start the thread
        //    //}
        //    //for (int i = 0; i < 10; i++)
        //    //{
        //    //    ThreadStart ts = new ThreadStart(() => { someInt++; Console.WriteLine(someInt); });     //Create a thread start given delegate
        //    //    Thread t = new Thread(ts);                          //Create thread with threadstart
        //    //    t.Start();                                          //Start the thread
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(() => { someInt++; });     //Create a thread start given delegate
        //    //    Thread t = new Thread(ts);                          //Create thread with threadstart
        //    //    t.Start();                                          //Start the thread
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(() => { PrintOutChar('a', 50); });
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(() => { PrintOutChar('a', 50); });
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}
        //    //for (int i = 0; i < 5; i++)
        //    //{
        //    //    Thread.Sleep(500);
        //    //    ThreadStart ts = new ThreadStart(NightClub);
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(NightClub);
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(BiggerNightClub);
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(BiggerNightClub);
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    //{
        //    //    ThreadStart ts = new ThreadStart(BiggerNightClub);
        //    //    Thread t = new Thread(ts);
        //    //    t.Start();
        //    //}

        //    List<delg> delgList = new List<delg>()
        //    {
        //        BiggerNightClub,
        //        BiggerNightClub,
        //        NightClub,
        //        () => someInt++,
        //        () => BiggerNightClub(),
        //        () => PrintOutChar('a', 50)
        //    };

        //    Stack<delg> toProcess = new Stack<delg>(delgList);

        //    while (toProcess.Count > 0)
        //    {
        //        delg nextToProc = toProcess.Pop();
        //        StartThread(nextToProc);
        //    }


        //    Console.WriteLine("Program finished");
        //    Console.ReadLine();
        //}

        public static void StartThread(delg d)
        {
            ThreadStart ts = new ThreadStart(d);
            Thread t = new Thread(ts);
            t.Start();
        }

        public static void BiggerNightClub()
        {
            if (sem.WaitOne(1000))
            {
                Console.WriteLine("Inside the bigger club");
                Thread.Sleep(2000);
                Console.WriteLine("Exiting the bigger club");
                sem.Release();
            }
            else
            {
                Console.WriteLine("Got bored and left");
            }
        }

        public static void NightClub()
        {
            if (mutex.WaitOne(1000))
            {
                Console.WriteLine("Inside the club");
                Thread.Sleep(2000);
                Console.WriteLine("Exiting club");
                mutex.ReleaseMutex();
            }
            else
            {
                Console.WriteLine("Got bored and left");
            }

        }

        public static void GoatNoises(int id)
        {
            lock (myLock)
            {
                Thread.Sleep(3000);
                Console.WriteLine("baaah " + id);
                //Console.WriteLine("some int: " + someInt);
            }
        }

        public static void PrintOutOnes()
        {
            for (int i = 0; i < 15; ++i)
            {
                Console.WriteLine("1");
                someInt++;
            }
        }

        public static void PrintOutZeros()
        {
            for (int i = 0; i < 15; ++i)
            {
                someInt++;
                Console.WriteLine("0");
            }
        }

        public static void PrintOutChar(char toPrint, int numOfTimes)
        {
            for (int i = 0; i < numOfTimes; ++i)
            {
                Interlocked.Increment(ref someInt);
                Console.WriteLine(toPrint);
            }
        }

    }
}
