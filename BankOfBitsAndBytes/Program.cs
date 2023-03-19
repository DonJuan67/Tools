using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    class Program
    {
        static readonly int passwordLength = 6; //Can you solve up to 6?
        static int robbedAmount = 0;
        static bool done = false;

        static int nbThreads = 7;
        static Thread[] threads = new Thread[nbThreads];
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            int indexStarterStart = (BankOfBitsNBytes.acceptablePasswordChars.Length - 1) / nbThreads;
            int indexStarter = 0;
            for (int i = 0; i < threads.Length; i++)
            {
                int id = i;
                ThreadStart ts = new ThreadStart(() => { GeneratePassword(BankOfBitsNBytes.acceptablePasswordChars[indexStarter], bbb, id); });
                Thread t = new Thread(ts);
                threads[i] = new Thread(ts);
                threads[i].Start();
                threads[i].Join();


                indexStarter += indexStarterStart;
            }

            int z = 0;
            bool isStillAlive = true;
            while (isStillAlive)
            {
                isStillAlive = false;
                for (int i = 0; i < threads.Length; i++)
                {
                    if (threads[i].IsAlive)
                        isStillAlive = true;
                }
            }
            sw.Stop();
            Console.WriteLine("TIME: " + sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static void StopThreads(Thread[] threads)
        {
            if (robbedAmount == -1)
            {
                for (int i = 0; i < threads.Length; i++)
                {
                    if (threads[i] != null)
                        threads[i].Interrupt();
                }
            }
        }

        static void GeneratePassword(char startingChar, BankOfBitsNBytes bbb, int id)
        {
            char[] guess = new char[passwordLength];
            int forSecurity = 0;
            char c = startingChar;
            ResetPasswordGenerator(guess);
            while (robbedAmount != -1)
            {
                guess[0] = c;
                if (passwordLength > 1)
                {
                    while (BankOfBitsNBytes.acceptablePasswordChars.Contains(guess[1]))
                    {
                        char pastTheLastChar = GetTheLastChar();
                        pastTheLastChar = ++pastTheLastChar;

                        int previousIndex = (guess.Length - 1) - 1;
                        if (previousIndex > 0)
                        {
                            for (int i = guess.Length - 1; i > 1; i--)
                            {
                                if (guess[i] > GetTheLastChar())
                                {
                                    guess[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
                                    guess[i - 1] = ++guess[i - 1];
                                }
                            }
                            if (guess[1] == pastTheLastChar)
                            {
                                break;
                            }
                        }
                        for (int j = 0; j < BankOfBitsNBytes.acceptablePasswordChars.Length; j++)
                        {
                            guess[guess.Length - 1] = BankOfBitsNBytes.acceptablePasswordChars[j];
                            //OutputCharArray(guess, id);
                            if (!done)
                                robbedAmount = bbb.WithdrawMoney(guess);
                            if (robbedAmount == -1)
                            {
                                done = true;
                                StopThreads(threads);
                                return;
                            }
                        }
                        guess[guess.Length - 1] = ++guess[guess.Length - 1];
                    }
                }
                else
                {
                    //OutputCharArray(guess, id);
                    if (!done)
                        robbedAmount = bbb.WithdrawMoney(guess);
                    if (robbedAmount == -1)
                    {
                        done = true;
                        StopThreads(threads);
                        return;
                    }
                }
                ResetPasswordGenerator(guess);


                if (c == GetTheLastChar())
                    c = BankOfBitsNBytes.acceptablePasswordChars[0];
                else
                    c++;
                if (forSecurity >= 500000)
                {
                    Console.WriteLine("Please dont break my computer");
                    break;
                }
                forSecurity++;
            }
            //return toRet;
        }

        private static char GetTheLastChar()
        {
            return BankOfBitsNBytes.acceptablePasswordChars[BankOfBitsNBytes.acceptablePasswordChars.Length - 1];
        }

        private static void ResetPasswordGenerator(char[] guess)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                guess[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
            }
        }

        static Random r = new Random(); //To prevent it being re-created every frame based on sys clock (Which would produce non-random number)
        static char[] GenerateRandomPassword(int passwordLength)
        {
            char[] toRet = new char[passwordLength];
            for (int i = 0; i < passwordLength; ++i)
            {
                int randomInt = (r.Next() % BankOfBitsNBytes.acceptablePasswordChars.Length);
                toRet[i] = BankOfBitsNBytes.acceptablePasswordChars[randomInt];
            }
            return toRet;
        }

        //This is very expensive and just for debugging. You do not need to output in the final test
        static void OutputCharArray(char[] toOut, int id)
        {
            Console.Out.Write(id + ": ");
            Console.Out.WriteLine(new string(toOut));
        }
    }
}
