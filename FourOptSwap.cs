using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class FourOptSwap
    {
        public static Tour GetFourOptSwap(Tour tourToChange, int a, int b, int c, int d)
        {

            Tour returnTour = new Tour();
            List<int> LIST_A = new List<int>();
            List<int> LIST_B = new List<int>();
            List<int> LIST_C = new List<int>();
            List<int> LIST_D = new List<int>();

            for (int i = 0; i <= a ; i++)
            {
                LIST_A.Add(tourToChange.Cities[i]);
            }
            for (int i = a + 1; i <= b; i++)
            {
                LIST_B.Add(tourToChange.Cities[i]);
            }
            for (int i = b + 1; i <= c; i++)
            {
                LIST_C.Add(tourToChange.Cities[i]);
            }
            for (int i = c + 1; i <= tourToChange.Size - 2; i++)
            {
                LIST_D.Add(tourToChange.Cities[i]);
            }

            foreach (int city in LIST_A)
            {
                returnTour.AddNewCity(city);
            }
            foreach (int city in LIST_D)
            {
                returnTour.AddNewCity(city);
            }
            foreach (int city in LIST_C)
            {
                returnTour.AddNewCity(city);
            }
            foreach (int city in LIST_B)
            {
                returnTour.AddNewCity(city);
            }
            returnTour.AddNewCity(returnTour.Cities[0]); 

            Console.WriteLine(tourToChange.ToString());
            //Console.WriteLine("AB: " + a + "::" + b);
            Console.WriteLine(returnTour.ToString());

            return returnTour;
        }
    }
}