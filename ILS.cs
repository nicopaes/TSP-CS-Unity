using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TPS_IA
{
    public static class ILS
    {
        public static float IteratedLocalSearch(tspclass tsp, Tour StartTour, out int newBestIterations, out int swapIterations, out int goodSwapIterations, out Tour bestTour)
        {
            newBestIterations = 0;
            goodSwapIterations = 0;
            swapIterations = 0;
            int limitTwoOpt = 0;
            int limitFourOpt = 0;
            int ILS = 0;
            bestTour = null;
            Stopwatch stopWatch = new Stopwatch();


            if (tsp.Dimension + 1 != StartTour.Size)
            {
                newBestIterations = -1;
                bestTour = null;
                Console.WriteLine("Sometrhing is wrong with your tour mate");
                return -1; //Start tour have the wrong size for this particular tsp
            }


            float bestDistance = tsp.GetTourDistance(StartTour);
            Tour swapedTour = StartTour;

            //LAST BRUTE 2000 && 100000
            //while (newBestIterations < 50 && limit < 250 && swapIterations < 1)
            //(bestDistance != tsp.OptDistance && limit < 500)
            stopWatch.Start();
            while (/*ILS < 50 && */bestDistance != tsp.OptDistance && stopWatch.ElapsedMilliseconds <= 300000)//(bestDistance != tsp.OptDistance)
            {
                if (ILS != 0)
                {
                    while (limitFourOpt <= 500 && bestDistance != tsp.OptDistance)
                    {
                        //if(limitFourOpt%50 == 0) Console.WriteLine("4OPT" + limitFourOpt);
                        List<int> fourOptRandom = Utils.GenerateRandomSet(tsp.Dimension);
                        swapedTour = FourOptSwap.GetFourOptSwap(swapedTour, fourOptRandom[0], fourOptRandom[1], fourOptRandom[2], fourOptRandom[3]);
                        float newDistance = tsp.GetTourDistance(swapedTour);
                        if (newDistance >= bestDistance)
                        {
                            limitFourOpt++;
                            continue;
                        }
                        else
                        {
                            bestDistance = newDistance;
                            bestTour = swapedTour;
                            newBestIterations++;
                            limitFourOpt++;
                            
                            break;
                        }
                    }
                    limitTwoOpt = 0;
                    limitFourOpt = 0;
                }

                while (limitTwoOpt <= 8000 && bestDistance != tsp.OptDistance)
                {
                    for (var i = 1; i < StartTour.Size - 1; i++)
                    {
                        for (var k = i + 1; k < StartTour.Size - 1; k++)
                        {
                            swapIterations++;
                            swapedTour = TwoOptSwap.GetTwoOptSwap(swapedTour, i, k);

                            if (!swapedTour.CheckStartEnd())
                            {
                                Console.WriteLine("Q");
                                continue;
                            }
                            goodSwapIterations++;
                            float newDistance = tsp.GetTourDistance(swapedTour);

                            //Console.WriteLine(swapedTour.ToString() + " D: " + newDistance);

                            if (newDistance >= bestDistance)
                            {
                                continue;
                            }
                            else //if (newDistance < bestDistance)
                            {
                                //We have a new winner
                                //Console.WriteLine("NEW BEST: " + newBestIterations + "  DISTANCE: " + newDistance + "\n");
                                //Console.WriteLine("L: " + limit + " SI: " + swapIterations + " BI: " + newBestIterations + "  NEW: " + newDistance + "  BEST: " + bestDistance);

                                bestDistance = newDistance;
                                bestTour = swapedTour;
                                newBestIterations++;
                                break;
                            }
                        }
                    }
                    if (limitTwoOpt % 2000 == 0)
                    {
                        //Utils.ClearCurrentConsoleLine();
                        //Console.WriteLine("  L: " + limitTwoOpt);
                    }
                    limitTwoOpt++;
                }
                // Console.WriteLine("AI: " + swapedTour);
                ILS++;
                // Utils.ClearCurrentConsoleLine();
                // Utils.ClearCurrentConsoleLine();
                if (ILS % 10 == 0) Console.WriteLine("-----------ILS: " + ILS);
                // Console.WriteLine();
            }
            stopWatch.Stop();
            Console.WriteLine("IT'S OVER: " + Utils.PrintStopwatch(stopWatch.Elapsed));
            return bestDistance;
        }
    }
}