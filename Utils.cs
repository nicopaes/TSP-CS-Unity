using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public static class Utils
    {
        public static Random allRand = new Random();
        public static List<int> GenerateStartList(int cityQtd)
        {
            //Console.WriteLine("QTD CITY " + cityQtd);
            if (cityQtd >= 0)
            {
                List<int> deckInt = new List<int>();
                List<int> shuffleDeckInt = new List<int>();

                for (int i = 0; i < cityQtd - 1; i++)
                {
                    deckInt.Add(i);
                }
                for (int i = 0; i < cityQtd - 1; i++)
                {
                    int randIndex = allRand.Next(0, deckInt.Count);
                    int rand = deckInt[randIndex];
                    shuffleDeckInt.Add(rand);
                    deckInt.RemoveAt(randIndex);
                }
                shuffleDeckInt.Add(shuffleDeckInt[0]);
                return shuffleDeckInt;
            }
            else
            {
                return null;
            }
        }

        public static List<int> GenerateRandomSet(int sizeSet, int sizeRandomSpace)
        {
            if (sizeSet >= 0)
            {
                List<int> deckInt = new List<int>();
                List<int> shuffleDeckInt = new List<int>();

                while (shuffleDeckInt.Count != sizeSet)
                {
                    int randIndex = allRand.Next(0, sizeRandomSpace-1);
                    if (!shuffleDeckInt.Contains(randIndex))
                    {
                        shuffleDeckInt.Add(randIndex);
                    }
                }
                shuffleDeckInt.Sort();
                return shuffleDeckInt;
            }
            else
            {
                return null;
            }
        }

        public static List<int> GenerateRandomSetNotZero(int sizeSet, int sizeRandomSpace)
        {
            if (sizeSet >= 0)
            {
                List<int> deckInt = new List<int>();
                List<int> shuffleDeckInt = new List<int>();

                while (shuffleDeckInt.Count != sizeSet)
                {
                    int randIndex = allRand.Next(1, sizeRandomSpace-1);
                    if (!shuffleDeckInt.Contains(randIndex))
                    {
                        shuffleDeckInt.Add(randIndex);
                    }
                }
                shuffleDeckInt.Sort();
                return shuffleDeckInt;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<int[]> Permut(int[] arr)
        {
            while (true)
            {
                yield return arr;
                var j = arr.Length - 2;
                while (j >= 0 && arr[j] >= arr[j + 1]) j--;

                if (j < 0) break;

                var l = arr.Length - 1;
                while (arr[j] >= arr[l]) l--;

                var tmp = arr[l]; arr[l] = arr[j]; arr[j] = tmp;

                var k = j + 1;
                l = arr.Length - 1;
                while (k < l)
                {
                    var t = arr[k];
                    arr[k] = arr[l];
                    arr[l] = t;
                    k++;
                    l--;
                }

            }

        }

        public static float UglyPermutBruteForce(tspclass tsp, out int permutCount)
        {
            float bestDistance = 0;
            permutCount = 0;

            List<int> startPermutList = new List<int>();
            for (int i = 0; i < tsp.Dimension; i++)
            {
                startPermutList.Add(i);
            }

            // foreach (int city in startPermutList)
            // {
            //     Console.Write(" " + city);
            // } Console.WriteLine("  -----\n");

            foreach (int[] test in Utils.Permut(startPermutList.ToArray()))
            {
                List<int> ArrayList = new List<int>(test);
                ArrayList.Add(ArrayList[0]);
                Tour newT = new Tour(tsp.Dimension + 1, ArrayList.ToArray());

                float newDistance = tsp.GetTourDistance(newT);

                if (permutCount == 0) bestDistance = newDistance;

                //Console.WriteLine(newT.ToString() + "::> " + newDistance);

                if (newDistance < bestDistance) bestDistance = newDistance;
                permutCount++;
            }

            return bestDistance;
        }

        public static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        public static string PrintStopwatch(TimeSpan ts)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            return elapsedTime;
        }

        public class ILSLimits
        {
            public int ILS;
            public int TwoOpt;
            public int FourOpt;
            public float Time;

            public ILSLimits(int iLS, int twoOpt, int fourOpt, float Time)
            {
                this.ILS = iLS;
                this.TwoOpt = twoOpt;
                this.FourOpt = fourOpt;
                this.Time = Time;
            }
        }

        public class SALimits
        {
            public int SA;
            public float StartTemp;
            public float TempMult;
            public float Time;

            public SALimits(int SA, float StartTemp, float TempMult, float Time)
            {
                this.SA = SA;
                this.StartTemp = StartTemp;
                this.TempMult = TempMult;
                this.Time = Time;
            }
        }

        public static double GetNextRand()
        {
            return allRand.NextDouble();
        }
    }
}