using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace GuessNumber
{
    public struct answer
    {
        public int A;
        public int B;
    }
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("********************************");
            Console.WriteLine("What number do you think it is ?");
            Console.WriteLine("********************************");
            Console.WriteLine();
            Console.WriteLine("Commands: \'Give up\' , \'Hint\'");
            Console.WriteLine("10 turns to guess correctly");
            Console.WriteLine();
            Console.WriteLine("A = Correct number and correct position");
            Console.WriteLine("B = Correct number but wrong position");

            bool again = true;

            while (again)
            {
                int[] targetNumber = GenerateRandomNumber();

                //Console.WriteLine(ConvertIntArrayToString(targetNumber));

                int currentScore = 100;

                int turn = 10;

                int hints = 0;

                while (turn > 0)
                {
                Start:
                    Console.Write("\nAnswer:");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "give up")
                    {
                        turn = 0;
                        currentScore = 0;
                        break;
                    }
                    else if (answer.ToLower() == "hint")
                    {
                        if (hints < 3)
                        {
                            Console.WriteLine("Number {0} answer: {1}", hints + 1, targetNumber[hints]);
                            currentScore -= 20;
                            hints++;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("No more hints!");
                            continue;
                        }
                    }

                    if (answer.Length != 4) continue;

                    foreach (char c in answer)
                    {
                        if (!Char.IsNumber(c))
                        {
                            goto Start;
                        }
                    }

                    answer currentAnswer = CompareNumbers(targetNumber, ConvertStringToIntArray(answer));
                    Console.WriteLine(currentAnswer.A + "A" + currentAnswer.B + "B");
                    if (currentAnswer.A == 4)
                    {
                        break;
                    }

                    currentScore -= 5;
                    turn--;
                }

                if (turn == 0)
                {
                    Console.WriteLine("Correct answer " + ConvertIntArrayToString(targetNumber));
                }
                else { 
                    Console.WriteLine("Your final score: " + currentScore); 
                }

                Console.WriteLine("Play again? (y/n)");

                if (Console.ReadLine().ToLower() != "y")
                {
                    again = false;
                }
            }
            Console.WriteLine("Game Over!");
            Console.ReadLine();
        }

        protected static int[] GenerateRandomNumber()
        {
            int[] RandomNumber = new int[4];
            Random seed = new Random();
            int i = 0;
            while (i < 4)
            {
                RandomNumber[i] = seed.Next(0, 10);

                int j = i - 1;
                while (j >= 0)
                {
                    if (RandomNumber[i] == RandomNumber[j])
                        i--;
                    j--;
                }
                i++;
            }
            return RandomNumber;
        }

        //TODO
        protected static answer CompareNumbers(int[] TargetNumber, int[] CompareNumber)
        {
            answer ReturnAnswer;
            ReturnAnswer.A = 0;
            ReturnAnswer.B = 0;

            bool[] compared = new bool[4];

            for (int b = 0; b < 4; b++)
            {
                compared[b] = false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (TargetNumber[i] == CompareNumber[i])
                {
                    compared[i] = true;
                    ReturnAnswer.A++;
                }
                for (int j = 0; j < 4; j++)
                {
                    if (TargetNumber[i] == CompareNumber[j] && i != j && !compared[i])
                    {
                        compared[i] = true;
                        ReturnAnswer.B++;
                    }
                }
            }
            return ReturnAnswer;
        }

        protected static int[] ConvertStringToIntArray(string String)
        {
            int[] IntValue = new int[String.Length];
            for (int i = 0; i < String.Length; i++)
            {
                try
                {
                    IntValue[i] = Convert.ToInt32(String[i].ToString());
                }
                catch (Exception ex)
                {
                    return null;
                    //  Console.WriteLine("You must input number");
                }

            }
            return IntValue;
        }

        protected static string ConvertIntArrayToString(int[] IntArray)
        {
            char[] CharArray = new char[IntArray.Length];
            for (int i = 0; i < CharArray.Length; i++)
            {
                CharArray[i] = Convert.ToChar(IntArray[i].ToString());
            }
            return new string(CharArray);
        }
    }
}