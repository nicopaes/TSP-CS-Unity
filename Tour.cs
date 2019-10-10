using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class Tour
    {
        public int Size
        {
            get { return Cities.Count; } 
        }
        public List<int> Cities;

        public Tour(int tourSize, int[] listCities)
        {
            Cities = new List<int>(tourSize);
            if (listCities.Length >= tourSize)
            {
                for (int i = 0; i < tourSize; i++)
                {
                    if(!AddNewCity(listCities[i])) 
                    {
                        Console.Error.Write("Tour already contains this city, cities must be unique in Tour");
                        break;
                    }
                }
            }
            else Console.Error.Write("List of Cities to small");
        }

        public Tour()
        {
            Cities = new List<int>();
        }

        public bool AddNewCity(int newCity)
        {
            if(!this.Cities.Contains(newCity))
            {
                Cities.Add(newCity);
                return true;
            }
            else if(this.Cities.Exists( x => Cities[0] == newCity))
            {
                Cities.Add(newCity);
                return true;
            }
            else
            {
                return false;
            }
                        
        }
        public override string ToString()
        {
            string returnString = "";
            foreach (int city in this.Cities)
            {
                returnString += " " + city;
            }
            return returnString;
        }
        
        public bool CheckStartEnd()
        {
            return Cities[0] == Cities[Size-1];
        }
    }

}