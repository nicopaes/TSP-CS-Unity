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
            Tour startTour = new Tour(18, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 0}.ToArray());

            Console.WriteLine("Please enter the name of the .tsp file");

            fileName = Console.ReadLine();
            Console.WriteLine("fileName:  " + fileName);
            Console.ReadKey();

            bool fileExists = File.Exists(@"tspjson/" + fileName + ".json");
            if (fileExists)
            {
                tspFile = JsonConvert.DeserializeObject<tspclass>(File.ReadAllText(@"tspjson/" + fileName + ".json"));
                ListTours.Add(new Tour(tspFile.TourSize, tspFile.OptTour));
            }
            else Console.WriteLine("File do not exist");

            Console.WriteLine("OptDistance  :" + tspFile.OptDistance);
            Console.WriteLine("OptTour  :" + tspFile.OptTour.Length);
            Console.WriteLine("Format  :" + tspFile.Format);

            foreach (int city in tspFile.OptTour)
            {
                Console.Write("  " + city);
            }
            #region Distance Matrix
            //DISTANCE MATRIX
            // Console.WriteLine("\nDistance Matrix");
            // foreach (int[] row in tspFile.DistanceMatrix)
            // {
            //     foreach (int i in row)
            //     {
            //         Console.Write("  " + i);
            //     }
            //     Console.WriteLine();
            // }
            #endregion

            //Console.WriteLine("TEST DISTANCE CITY\n");

            // //TEST TOUR
            // foreach (Tour tour in ListTours)
            // {
            //     foreach (int city in tour.Cities)
            //     {
            //         Console.Write(" " + city);
            //     } Console.WriteLine();
            // }
            // Console.WriteLine("TOUR 0 DISTANCE: " + tspFile.GetTourDistance(ListTours[0]) + "\n");

            // Tour SwapResult = TwoOptSwap.GetTwoOptSwap(swapTest,3,6);
            // foreach (int city in swapTest.Cities)
            // {
            //     Console.Write(" " + city);
            // } Console.WriteLine();
            // foreach (int city in SwapResult.Cities)
            // {
            //     Console.Write(" " + city);
            // } Console.WriteLine();

            //FULL TWO OPT SWAP
            Console.WriteLine("\nFULL TWO OPT SWAP\n");
            int twoOpBesttIterations = 0;
            int twoOpSwaptIterations = 0;

            Tour twoOptBestTour = new Tour();
            float twoOptBestDistance = TwoOptSwap.FullTwoOpt(tspFile, startTour, out twoOpBesttIterations,out twoOpSwaptIterations, out twoOptBestTour);


            foreach (int city in twoOptBestTour.Cities)
            {
                Console.Write(" " + city);
            }          
            Console.Write("  BEST TOUR DISTANCE: " + twoOptBestDistance + "\n");

            Console.WriteLine();
            Console.WriteLine("BEST ITERATIONS: " + twoOpBesttIterations);
            Console.WriteLine("SWAP ITERATIONS: " + twoOpSwaptIterations);
            Console.WriteLine("GOOD TOURS CHECKED " + TwoOptSwap.goodSwapedTours.Count);
            Console.WriteLine("TOTAL TOURS CHECKED " + TwoOptSwap.totalSwapedTours.Count);
            
            foreach (Tour t in TwoOptSwap.goodSwapedTours)
            {
                if(t.Cities[0] != 0) Console.WriteLine("SOMETHING IS DIFERENTE");
            }
        }
    }
}
