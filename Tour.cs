using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class Tour
    {
        public int Size;
        public List<int> Cities;

        public Tour(int tourSize, int[] listCities)
        {
            Size = tourSize;
            Cities = new List<int>(this.Size);
            if (listCities.Length >= tourSize)
            {
                for (int i = 0; i < Size; i++)
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
            else
            {
                return false;
            }
                        
        }

    }

}