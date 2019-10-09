using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TPS_IA
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseTPS parser = new ParseTPS(); 
            tspclass tspFile = null;
            string fileName;

            List<Tour> ListTours = new List<Tour>();
            //ListTours.Add(new Tour(7,new List<int>{0,1,2,3,4,5,6}.ToArray())); //TODO Change this to a proper Array
            Tour swapTest = new Tour(9, new List<int>{0,1,2,3,4,5,6,7,8,0}.ToArray());

            Console.WriteLine("Please enter the name of the .tsp file");

            fileName = Console.ReadLine();
            Console.WriteLine("fileName:  " + fileName);
            Console.ReadKey();

            bool fileExists = File.Exists(@"tspjson/" + fileName + ".json");
            if(fileExists) 
            {
                tspFile = JsonConvert.DeserializeObject<tspclass>(File.ReadAllText(@"tspjson/"+ fileName + ".json"));
                ListTours.Add(new Tour(tspFile.TourSize,tspFile.OptTour));
            }
            else Console.WriteLine("File do not exist");

            Console.WriteLine("OptDistance  :" + tspFile.OptDistance);
            Console.WriteLine("OptTour  :" + tspFile.OptTour.Length);
            Console.WriteLine("Format  :" + tspFile.Format);

            foreach (int city in tspFile.OptTour)
            {
                Console.Write("  " + city);
            }
            Console.WriteLine("\nDistance Matrix");
            foreach (int[] row in tspFile.DistanceMatrix)
            {
                foreach (int i in row)
                {
                    Console.Write("  " + i);
                }
                Console.WriteLine();
            }

            Console.WriteLine("TEST DISTANCE CITY\n");
           
            //TEST TOUR
            foreach (Tour tour in ListTours)
            {
                foreach (int city in tour.Cities)
                {
                    Console.Write(" " + city);
                } Console.WriteLine();
            }
            Console.WriteLine("TOUR 0 DISTANCE: " + tspFile.GetTourDistance(ListTours[0]) + "\n");

            Tour SwapResult = TwoOptSwap.GetTwoOptSwap(swapTest,3,6);
            foreach (int city in swapTest.Cities)
            {
                Console.Write(" " + city);
            } Console.WriteLine();
            foreach (int city in SwapResult.Cities)
            {
                Console.Write(" " + city);
            } Console.WriteLine();

        }
    }
}
