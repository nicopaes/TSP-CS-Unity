using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class TwoOptSwap
    {

        public static List<Tour> goodSwapedTours = new List<Tour>();
        public static List<Tour> totalSwapedTours = new List<Tour>();
        public static Tour GetTwoOptSwap(Tour tourToChange, int a, int b)
        {

            Tour returnTour = new Tour();
            List<int> tempList = new List<int>();


            for (int i = 0; i <= a - 1; i++)
            {
                returnTour.AddNewCity(tourToChange.Cities[i]);
            }
            for (int i = a; i <= b; i++)
            {
                tempList.Add(tourToChange.Cities[i]);
            }
            tempList.Reverse();

            foreach (int city in tempList)
            {
                returnTour.AddNewCity(city);
            }

            for (int i = b + 1; i < tourToChange.Size; i++)
            {
                returnTour.AddNewCity(tourToChange.Cities[i]);
            }
            //Console.WriteLine(tourToChange.ToString());
            //Console.WriteLine("AB: " + a + "::" + b);
            //Console.WriteLine(returnTour.ToString());

            totalSwapedTours.Add(returnTour);
            return returnTour;
        }

        public static float FullTwoOpt(tspclass tsp, Tour StartTour, out int newBestIterations, out int swapIterations, out Tour bestTour)
        {
            Console.WriteLine("\nHELLO\n");
            newBestIterations = 0;
            swapIterations = 0;
            int limit = 0;
            bestTour = null;

            if (tsp.Dimension + 1 != StartTour.Size)
            {
                newBestIterations = -1;
                bestTour = null;
                return -1; //Start tour have the wrong size for this particular tsp
            }


            float bestDistance = tsp.GetTourDistance(StartTour);
            Tour swapedTour = StartTour;

            //LAST BRUTE 2000 && 100000
            while (newBestIterations < 50 && limit < 100000)
            {
                for (var i = 1; i < StartTour.Size - 1; i++)
                {
                    for (var k = i + 1; k < StartTour.Size; k++)
                    {
                        swapIterations++;
                        swapedTour = GetTwoOptSwap(swapedTour, i, k);
                        //swapIterations+=1;
                        if (!swapedTour.CheckStartEnd())
                        {
                            //swapIterations++;
                            continue;
                        }
                        goodSwapedTours.Add(swapedTour);
                        float newDistance = tsp.GetTourDistance(swapedTour);

                        if (newDistance >= bestDistance)
                        {
                            continue;
                        }
                        else
                        {
                            //We have a new winner
                            //Console.WriteLine("B: " + bestDistance + "  ND: " + newDistance);
                            Console.WriteLine("L: " + limit + " SI: " + swapIterations + " BI: " + newBestIterations + "  BEST: " + bestDistance);
                            bestDistance = newDistance;
                            bestTour = swapedTour;
                            newBestIterations ++;
                        }
                    }
                }
                limit++;
                Console.WriteLine("L: " + limit + " SI: " + swapIterations + " BI: " + newBestIterations + "  BEST: " + bestDistance);
            }
            Console.WriteLine();
            return bestDistance;
        }
    }
}