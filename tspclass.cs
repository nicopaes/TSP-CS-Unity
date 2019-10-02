using System;

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

        public tspclass(int tourSize, int[] optTour, int optDistance)
        {
            TourSize = tourSize;
            OptTour = optTour;
            OptDistance = optDistance;
        }

        public float GetDistance(int cityA, int cityB)
        {
            if(this.Format.Equals(EdgeFormat.LOWER_DIAG_ROW))
            {
                if(cityA < this.Dimension || cityB < this.Dimension)
                {
                    return this.DistanceMatrix[cityA][cityB];
                    
                }
            }
            return -1; //Return -1 if the cities asked are out of bounds;
        }
    }
}