using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TPS_IA
{
    //Simulated Anneling
    public static class SA
    {
        public static float SimulatedAnneling(tspclass tsp, Tour StartTour, out int newBestIterations, out int TwoSwapIterations, out int FourSwapIterations, out int TempIterations, out Tour bestTour, Utils.SALimits Limit)
        {
            newBestIterations = 0;
            FourSwapIterations = 0;
            TwoSwapIterations = 0;

            TempIterations = 0;

            float currTemp;
            float tempMult = Limit.TempMult;

            int SA = 0;

            float bestDistance = tsp.GetTourDistance(StartTour);
            float currentDist, nextDist;
            float deltaDiff;

            Tour currentTour = StartTour;
            Tour nextTour = null;

            List<int> RandomSet = new List<int>();
            List<int> RandomSet2 = new List<int>();

            bestTour = null;
            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("START TEMP: " + Limit.StartTemp + " START MULT: " + Limit.TempMult);
            stopWatch.Start();
            while (bestDistance != tsp.OptDistance && stopWatch.ElapsedMilliseconds <= (Limit.Time * 1000f) && SA < Limit.SA)
            {
                currTemp = Limit.StartTemp; //* ((SA/10) +1);

                //Console.WriteLine(currTemp *= tempMult);

                // RandomSet2 = Utils.GenerateRandomSet(4, tsp.Dimension);
                // currentTour = FourOptSwap.GetFourOptSwap(currentTour, RandomSet2[0], RandomSet2[1], RandomSet2[2], RandomSet2[3]);

                while (currTemp > 0.0013f)
                {
                    RandomSet = Utils.GenerateRandomSetNotZero(2, tsp.Dimension);
                    nextTour = TwoOptSwap.GetTwoOptSwap(currentTour, RandomSet[0], RandomSet[1]);

                    //RandomSet2 = Utils.GenerateRandomSet(4, tsp.Dimension);
                    //nextTour = FourOptSwap.GetFourOptSwap(currentTour, RandomSet2[0], RandomSet2[1], RandomSet2[2], RandomSet2[3]);

                    //Console.WriteLine(nextTour.ToString());
                    TwoSwapIterations++;
                    currentDist = tsp.GetTourDistance(currentTour);
                    nextDist = tsp.GetTourDistance(nextTour);

                    deltaDiff = currentDist - nextDist;
                    if (deltaDiff > 0)
                    {
                        currentTour = nextTour;
                    }
                    else
                    {
                        if (Utils.GetNextRand() <= Math.Exp(deltaDiff / currTemp))
                        {
                            currentTour = nextTour;
                        }
                    }

                    if (currentDist < bestDistance)
                    {
                        bestDistance = currentDist;
                        bestTour = currentTour;
                        newBestIterations++;
                    }

                    currTemp *= tempMult;
                    //Console.WriteLine(currTemp);
                    TempIterations++;
                    if (TempIterations % 5000 == 0) Console.WriteLine("Current Temp: " + currTemp + "  :::>Current Best Tour Distance " + bestDistance);

                }
                SA++;
            }

            return bestDistance;
        }
    }
}