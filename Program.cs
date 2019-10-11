using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading;
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
            Stopwatch stopWatch = new Stopwatch();
            string fileName;

            List<Tour> ListTours = new List<Tour>();
            //ListTours.Add(new Tour(7,new List<int>{0,1,2,3,4,5,6}.ToArray())); //TODO Change this to a proper Array
            Tour startTour = new Tour(18, new List<int> { 15, 16, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 15}.ToArray());
            //Tour startTour = new Tour(6, new List<int> { 2, 1, 3, 5, 4, 2}.ToArray());
            //Tour startTour = new Tour(6, new List<int> { 0, 1, 2, 3, 4, 0 }.ToArray());
            //Tour startTour = new Tour(7, new List<int> { 0, 1, 2, 3, 4, 5, 0 }.ToArray());

            // for (int i = 0; i < 4; i++)
            // {
            //     List<int> tempList = Utils.GenerateStartList(6);
            //     ListTours.Add(new Tour(6, tempList.ToArray()));
            // }
            // int tc = 0;
            // foreach (Tour tour in ListTours)
            // {
            //     Console.WriteLine(tour.ToString());
            //     Console.Write("TOUR LIST  " + tc + "\n");
            //     tc++;
            // }

            Console.WriteLine("\nPlease enter the name of the .tsp file\n");

            // OTHER FILES JSON
            // fileName = Console.ReadLine();
            // Console.WriteLine("fileName:  " + fileName);
            // Console.ReadKey();

            fileName = "gr17";

            bool fileExists = File.Exists(@"tspjson/" + fileName + ".json");
            if (fileExists)
            {
                tspFile = JsonConvert.DeserializeObject<tspclass>(File.ReadAllText(@"tspjson/" + fileName + ".json"));
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
            Console.WriteLine("\nDistance Matrix");
            foreach (int[] row in tspFile.DistanceMatrix)
            {
                foreach (int i in row)
                {
                    Console.Write("  " + i);
                }
                Console.WriteLine();
            }
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
            //Tour startTour = new Tour(tspFile.Dimension+1, Utils.GenerateStartList(tspFile.Dimension+1).ToArray());
            ListTours.Add(startTour);
            Tour startTourFList = ListTours[0];

            Console.WriteLine("\nFULL TWO OPT SWAP\n\n");
            int twoOpBesttIterations = 0;
            int twoOpSwaptIterations = 0;
            int twoOpGoodSwaptIterations = 0;

            Tour twoOptBestTour = new Tour();
            stopWatch.Start();
            float twoOptBestDistance = ILS.IteratedLocalSearch(tspFile, startTourFList, out twoOpBesttIterations, out twoOpSwaptIterations, out twoOpGoodSwaptIterations, out twoOptBestTour);
            stopWatch.Stop();
            Console.WriteLine("TIME: " + Utils.PrintStopwatch(stopWatch.Elapsed));

            Console.Write("::> " + startTourFList.ToString() + "  START TOUR DISTANCE: " + tspFile.GetTourDistance(startTourFList) + "\n");
            if (twoOpBesttIterations != -1)
            {
                Console.Write("::> " + twoOptBestTour.ToString() + "  BEST TOUR DISTANCE: " + twoOptBestDistance + "\n");
                Console.Write("::> " + tspFile.PrintOptTour() + "  OPTIMAL TOUR DISTANCE: " + tspFile.OptDistance + "\n");

                Console.WriteLine();
                Console.WriteLine("BEST ITERATIONS: " + twoOpBesttIterations);
                Console.WriteLine("GOOD ITERATIONS: " + twoOpGoodSwaptIterations);
                Console.WriteLine("TOTAL SWAP ITERATIONS: " + twoOpSwaptIterations);
            }
            Console.WriteLine();

            int permutcount = 0;
            //float uglyPermut = Utils.UglyPermutBruteForce(tspFile,out permutcount);
            //Console.WriteLine("UGLY PERMUT: " + uglyPermut + "::" + permutcount);

            // foreach (int[] test in Utils.Permut(new int[] { 0, 1, 2, 4, 5 }))
            // {
            //     List<int> ArrayList = test.ToList();
            //     ArrayList.Add(ArrayList[0]);

            //     foreach (int city in ArrayList)
            //     {
            //         Console.Write(" " + city);
            //     }
            //     permutcount++;
            //     Console.WriteLine();
            // }
            Console.WriteLine("PERMUT COUNT: " + permutcount);

            Tour testFourOptTour = FourOptSwap.GetFourOptSwap(new Tour(8,new List<int>{0,1,2,3,4,5,6,0}.ToArray()),1,3,5,6);
            Console.WriteLine(" 4 OPT TEST: " + testFourOptTour.ToString());
        }
    }
}
