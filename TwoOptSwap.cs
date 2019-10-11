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

            //totalSwapedTours.Add(returnTour);
            return returnTour;
        }


        public static float FullTwoOpt(tspclass tsp, Tour StartTour, out int newBestIterations, out int swapIterations, out int goodSwapIterations, out Tour bestTour)
        {
            newBestIterations = 0;
            goodSwapIterations = 0;
            swapIterations = 0;
            int limit = 0;
            bestTour = null;

            if (tsp.Dimension + 1 != StartTour.Size)
            {
                newBestIterations = -1;
                bestTour = null;
                Console.WriteLine("Sometrhing is wrong with your tour mate");
                return -1; //Start tour have the wrong size for this particular tsp
            }


            float bestDistance = tsp.GetTourDistance(StartTour);
            Tour swapedTour = StartTour;

            //LAST BRUTE 2000 && 100000
            //while (newBestIterations < 50 && limit < 250 && swapIterations < 1)
            while(bestDistance != tsp.OptDistance && limit < 500)
            {
                for (var i = 1; i < StartTour.Size; i++)
                {
                    for (var k = i + 1; k < StartTour.Size; k++)
                    {
                        swapIterations++;
                        swapedTour = GetTwoOptSwap(swapedTour, i, k);

                        if (!swapedTour.CheckStartEnd())
                        {
                            continue;
                        }
                        goodSwapIterations++;
                        float newDistance = tsp.GetTourDistance(swapedTour);
                        
                        //Console.WriteLine(swapedTour.ToString() + " D: " + newDistance);

                        if (newDistance >= bestDistance)
                        {
                            continue;
                        }
                        else //if (newDistance < bestDistance)
                        {
                            //We have a new winner
                            //Console.WriteLine("B: " + bestDistance + "  ND: " + newDistance);
                            //Console.WriteLine("L: " + limit + " SI: " + swapIterations + " BI: " + newBestIterations + "  NEW: " + newDistance + "  BEST: " + bestDistance);
                            bestDistance = newDistance;
                            bestTour = swapedTour;
                            newBestIterations ++;
                        }
                    }
                }
                limit++;
                //Console.WriteLine("L: " + limit + " SI: " + swapIterations + " BI: " + newBestIterations + "  BEST: " + bestDistance + ":: " +  swapedTour.ToString());
            }
            Console.WriteLine();
            return bestDistance;
        }
    }
}