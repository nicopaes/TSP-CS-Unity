using System;
using System.IO;
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

            Console.WriteLine("Please enter the name of the .tsp file");

            fileName = Console.ReadLine();
            Console.WriteLine("fileName:  " + fileName);
            Console.ReadKey();

            bool fileExists = File.Exists(@"tspjson/" + fileName + ".json");
            if(fileExists) tspFile = JsonConvert.DeserializeObject<tspclass>(File.ReadAllText(@"tspjson/"+ fileName + ".json"));
            else Console.WriteLine("File do not exist");

            Console.WriteLine("OptDistance  :" + tspFile.OptDistance);
            Console.WriteLine("OptTour  :" + tspFile.OptTour.Length);
            Console.WriteLine("Format  :" + tspFile.Format);

            foreach (int point in tspFile.OptTour)
            {
                Console.Write("  " + point);
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
            //TEST CITY A
            for (int i = 0; i < tspFile.Dimension; i++)
            {
                Console.Write("  " + tspFile.GetDistance(1,i));
            }
        }
    }
}
