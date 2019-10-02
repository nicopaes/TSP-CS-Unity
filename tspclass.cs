using System;

namespace TPS_IA
{
    public class tspclass
    {
        public int TourSize;
        public int[] OptTour;
        public int OptDistance;

        public tspclass(int tourSize, int[] optTour, int optDistance)
        {
            TourSize = tourSize;
            OptTour = optTour;
            OptDistance = optDistance;
        }
    }
}