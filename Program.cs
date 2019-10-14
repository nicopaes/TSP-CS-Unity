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
            int typeOfSearch = 0;

            List<Tour> ListTours = new List<Tour>();
            //ListTours.Add(new Tour(7,new List<int>{0,1,2,3,4,5,6}.ToArray())); //TODO Change this to a proper Array
            //Tour startTour = new Tour(18, new List<int> { 15, 16, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 15 }.ToArray());

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

            //OTHER FILES JSON
            fileName = Console.ReadLine();
            Console.WriteLine("fileName:  " + fileName);
            Console.ReadKey();

            //fileName = "gr17";

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

            Console.WriteLine("SELECIONE O TIPO DE BUSCA\n");
            Console.WriteLine("1 - Iterated Local Search\n");
            Console.WriteLine("2 - Simulated Anneling\n");
            Console.WriteLine("-1 Encerrar o programa\n");

            typeOfSearch = Convert.ToInt32(Console.ReadLine());

            while (typeOfSearch != -1)
            {
                Tour startTour = new Tour(tspFile.Dimension + 1, Utils.GenerateStartList(tspFile.Dimension + 1).ToArray());
                ListTours.Add(startTour);
                Console.WriteLine("RAND");

                Tour startTourFList = new Tour(tspFile.Dimension + 1, Utils.GenerateStartList(tspFile.Dimension + 1).ToArray());//ListTours[0];
                while(startTourFList.Cities[0] != tspFile.OptTour[0])
                {
                    startTourFList = new Tour(tspFile.Dimension + 1, Utils.GenerateStartList(tspFile.Dimension + 1).ToArray());//ListTours[0];
                }

                if (typeOfSearch == 1)
                {


                    Console.WriteLine("\nILS\n");
                    int twoOptBesttIterations = 0;
                    int twoOptSwaptIterations = 0;
                    int fourOptSwaptIterations = 0;

                    Tour twoOptBestTour = new Tour();
                    Utils.ILSLimits Limit1 = new Utils.ILSLimits(100, 8000, 1, 120);
                    stopWatch.Start();
                    float twoOptBestDistance = ILS.IteratedLocalSearch(tspFile, startTourFList, out twoOptBesttIterations, out twoOptSwaptIterations, out fourOptSwaptIterations, out twoOptBestTour, Limit1);
                    stopWatch.Stop();
                    Console.WriteLine("TIME: " + Utils.PrintStopwatch(stopWatch.Elapsed));

                    Console.Write("::> " + startTourFList.ToString() + "  START TOUR DISTANCE: " + tspFile.GetTourDistance(startTourFList) + "\n");
                    if (twoOptBesttIterations != -1)
                    {
                        Console.Write("::> " + twoOptBestTour.ToString() + "  BEST TOUR DISTANCE: " + twoOptBestDistance + "\n");
                        Console.Write("::> " + tspFile.PrintOptTour() + "  OPTIMAL TOUR DISTANCE: " + tspFile.OptDistance + "\n");

                        Console.WriteLine();
                        Console.WriteLine("BEST ITERATIONS: " + twoOptBesttIterations);
                        Console.WriteLine("TOTAL FOUR SWAP ITERATIONS: " + fourOptSwaptIterations);
                        Console.WriteLine("TOTAL TWO SWAP ITERATIONS: " + twoOptSwaptIterations);
                    }
                    Console.WriteLine();
                }
                else if (typeOfSearch == 2)
                {


                    Console.WriteLine("\nSA\n");

                    int saBesttIterations = 0;
                    int saSwaptIterations = 0;
                    int saFourSwaptIterations = 0;
                    int saTempIterations = 0;

                    Tour SABestTour = new Tour();
                    Utils.SALimits Limit2 = new Utils.SALimits(200, 500, 0.994f, 120);
                    stopWatch.Start();
                    float SABestDistance = SA.SimulatedAnneling(tspFile, startTourFList, out saBesttIterations, out saSwaptIterations, out saFourSwaptIterations, out saTempIterations, out SABestTour, Limit2);
                    stopWatch.Stop();
                    Console.WriteLine("TIME: " + Utils.PrintStopwatch(stopWatch.Elapsed) + "  MS: " + stopWatch.ElapsedMilliseconds);

                    Console.Write("::> " + startTourFList.ToString() + "  START TOUR DISTANCE: " + tspFile.GetTourDistance(startTourFList) + "\n");
                    if (saBesttIterations != -1)
                    {
                        Console.Write("::> " + SABestTour.ToString() + "  BEST TOUR DISTANCE: " + SABestDistance + "\n");
                        Console.Write("::> " + tspFile.PrintOptTour() + "  OPTIMAL TOUR DISTANCE: " + tspFile.OptDistance + "\n");

                        Console.WriteLine();
                        Console.WriteLine("BEST ITERATIONS: " + saBesttIterations);
                        Console.WriteLine("TOTAL TEMP SWAP ITERATIONS: " + saTempIterations);
                        Console.WriteLine("TOTAL TWO SWAP ITERATIONS: " + saSwaptIterations);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("SELECIONE O TIPO DE BUSCA\n");
                    Console.WriteLine("1 - Iterated Local Search\n");
                    Console.WriteLine("2 - Simulated Anneling\n");
                    Console.WriteLine("-1 Encerrar o programa\n");

                    typeOfSearch = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("SELECIONE O TIPO DE BUSCA\n");
                Console.WriteLine("1 - Iterated Local Search\n");
                Console.WriteLine("2 - Simulated Anneling\n");
                Console.WriteLine("-1 - Encerrar o programa\n");

                typeOfSearch = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
