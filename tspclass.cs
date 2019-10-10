using System;
using System.Collections.Generic;

namespace TPS_IA
{
    public class tspclass
    {
        public enum EdgeFormat
        {
            FULL_MATRIX,
            UPPER_ROW,
            LOWER_ROW,
            UPPER_DIAG_ROW,
            LOWER_DIAG_ROW,
            UPPER_COL,
            LOWER_COL,
            UPPER_DIAG_COL,
            LOWER_DIAG_COL
        }
        public EdgeFormat Format;
        public int Dimension;
        public int TourSize;
        public int[] OptTour;
        public int OptDistance;
        public int[][] DistanceMatrix;

        public tspclass()
        {
            //     TourSize = tourSize;
            //     OptTour = optTour;
            //     OptDistance = optDistance;

        }

        public float GetDistance(int cityA, int cityB)
        {
            if (this.Format.Equals(EdgeFormat.LOWER_DIAG_ROW))
            {
                if (cityA < this.Dimension || cityB < this.Dimension)
                {
                    return this.DistanceMatrix[cityA][cityB];
                }
            }
            return -1; //Return -1 if the cities asked are out of bounds;
        }

        public float GetTourDistance(Tour tour)
        {
            float totalDistance = 0;
            //Console.WriteLine("GET DISTANCE: " + tour.ToString() + "::" + tour.Size);
            for (int i = 0; i < tour.Size - 1; i++)
            {
                float edgeDistance = GetDistance(tour.Cities[i], tour.Cities[i + 1]);
                totalDistance += edgeDistance;
                //Console.WriteLine(tour.Cities[i] + "==>" + tour.Cities[i + 1] + " ::> " + edgeDistance);
            }
            float lastEdge = GetDistance(tour.Cities[tour.Cities.Count -1], tour.Cities[0]);
            totalDistance += lastEdge; //Last city back to first city

            //Console.WriteLine(tour.Cities[tour.Cities.Count - 1] + "==>0 ::> " + lastEdge + "\n");

            return totalDistance;
        }
    }
}