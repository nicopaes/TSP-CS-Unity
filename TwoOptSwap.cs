using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class TwoOptSwap
    {
        public static Tour GetTwoOptSwap(Tour tourToChange,int a,int b)
        {
            Tour returnTour = new Tour();

            List<int> tempList = new List<int>();
            Console.WriteLine(returnTour.Cities.Count);
            for (int i = 0; i < a; i++)
            {
                returnTour.AddNewCity(tourToChange.Cities[i]);
            }
            for (int i = a; i < b; i++)
            {
                tempList.Add(tourToChange.Cities[i]);
            } tempList.Reverse();

            foreach (int city in tempList)
            {
                returnTour.AddNewCity(city);
            }

            for (int i = b-1; i < tourToChange.Size-1; i++)
            {
                returnTour.AddNewCity(tourToChange.Cities[i]);
            }
            return returnTour;
        }
    }
}