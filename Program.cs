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
            tspclass gr17;
            string fileName;

            fileName = Console.ReadLine();
            Console.WriteLine("fileName:  " + fileName);
            Console.ReadKey();

            gr17 = JsonConvert.DeserializeObject<tspclass>(File.ReadAllText(@"tspjson/"+ fileName + ".json"));


            Console.WriteLine("PARSE TEST  :" + gr17.OptDistance);
            Console.WriteLine("OptTour  :" + gr17.OptTour.Length);

            foreach (int point in gr17.OptTour)
            {
                Console.Write("  " + point);
            }
        }
    }
}
