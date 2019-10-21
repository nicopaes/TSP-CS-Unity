using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TPS_IA
{
    //Iterated Local Search
    public static class ILS
    {
        public static float IteratedLocalSearch(tspclass tsp, Tour StartTour, out int newBestIterations, out int TwoSwapIterations, out int FourSwapIterations, out Tour bestTour, Utils.ILSLimits Limit)
        {
            newBestIterations = 0;
            FourSwapIterations = 0;
            TwoSwapIterations = 0;

            int limitTwoOpt = 0;

            int limitFourOpt = 0;

            int ILS = 0;

            bestTour = null;
            Stopwatch stopWatch = new Stopwatch();
            List<Tour> twoOptList = new List<Tour>();


            if (tsp.Dimension + 1 != StartTour.Size)
            {
                newBestIterations = -1;
                bestTour = null;
                Console.WriteLine("Something is wrong with your tour mate");
                return -1; //Start tour have the wrong size for this particular tsp
            }


            float bestDistance = tsp.GetTourDistance(StartTour);
            Tour swapedTour = StartTour;

            Console.WriteLine("ILS RUNNING -- TIME LIMIT: " + Limit.Time + " TWOOPT: " + Limit.TwoOpt);
            stopWatch.Start();
            while (ILS < Limit.ILS && bestDistance != tsp.OptDistance && stopWatch.ElapsedMilliseconds <= (Limit.Time * 1000f))//(bestDistance != tsp.OptDistance)
            {
                if (ILS != 0)
                {
                    while (limitFourOpt <= Limit.FourOpt && bestDistance != tsp.OptDistance)
                    {
                        //if(limitFourOpt%50 == 0) Console.WriteLine("4OPT" + limitFourOpt);
                        List<int> fourOptRandom = Utils.GenerateRandomSet(4,tsp.Dimension);
                        // foreach (int i in fourOptRandom)
                        // {
                        //     Console.Write(" " + i);
                        // } Console.WriteLine();
                        swapedTour = FourOptSwap.GetFourOptSwap(swapedTour, fourOptRandom[0], fourOptRandom[1], fourOptRandom[2], fourOptRandom[3]);
                        FourSwapIterations++;
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

                while (limitTwoOpt <= Limit.TwoOpt && bestDistance != tsp.OptDistance)
                {
                    for (var i = 1; i < StartTour.Size - 1; i++)
                    {
                        for (var k = i + 1; k < StartTour.Size - 1; k++)
                        {
                            TwoSwapIterations++;
                            swapedTour = TwoOptSwap.GetTwoOptSwap(swapedTour, i, k);

                            // if(twoOptList.FindIndex( x => (x == swapedTour)) 
                            // {
                            //     Console.WriteLine(swapedTour.ToString());
                            //     Console.WriteLine("You're meh");
                            //     Environment.Exit(-1);
                            // }
                            twoOptList.Add(swapedTour);
                            float newDistance = tsp.GetTourDistance(swapedTour);

                           

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
                if (ILS % 10 == 0 || stopWatch.ElapsedMilliseconds%10000 == 0) Console.WriteLine("-----------ILS: " + ILS);
                // Console.WriteLine();
            }
            stopWatch.Stop();
            Console.WriteLine("IT'S OVER: " + Utils.PrintStopwatch(stopWatch.Elapsed));
            return bestDistance;
        }
    }
}