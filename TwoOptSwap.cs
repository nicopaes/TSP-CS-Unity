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
            //Console.WriteLine("2C: " + tourToChange.ToString());

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

            for (int i = b + 1; i < tourToChange.Size - 1; i++)
            {
                returnTour.AddNewCity(tourToChange.Cities[i]);
            }
            returnTour.AddNewCity(returnTour.Cities[0]);

            if (returnTour.Size > tourToChange.Size || !returnTour.CheckStartEnd())
            {
                Console.WriteLine(tourToChange.ToString() + " NOPE " + returnTour.ToString() + "AB:" + a + ":" + b + "\n");
            }
            //Console.WriteLine(tourToChange.ToString());
            //Console.WriteLine("AB: " + a + "::" + b);
            //Console.WriteLine(returnTour.ToString());

            //totalSwapedTours.Add(returnTour);
            return returnTour;
        }
    }
}