namespace GameBalanceModeling
{
    public class Program
    {
        static void Main()
        {
            StartingConditions startingConditions = new();

            int maxTimeDifference = 0;
            double bestTimePossible = double.MaxValue;

            while (startingConditions.Next())
            {
                GameState gameState = new(startingConditions);
                StratSet allStrats = RecursivePlayer.GetAllStrats(gameState);
                allStrats.Sort();

                int timeDifference = (int)(allStrats[1].Time - allStrats[0].Time);

                bool greaterTimeDifference = timeDifference > maxTimeDifference;
                bool smallerOverallTime = (timeDifference == maxTimeDifference) && (allStrats[0].Time < bestTimePossible);
                bool prefferedTimeDifference = allStrats[0].HasDoubleIncomeInTheMiddle && (greaterTimeDifference || smallerOverallTime);

                if (prefferedTimeDifference)
                {
                    bestTimePossible = allStrats[0].Time;
                    maxTimeDifference = timeDifference;
                    Console.WriteLine($"timeDifference {timeDifference}:");
                    allStrats.PrintSummaries();
                    startingConditions.Print();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            /*
            double fastestTime = RecursivePlayer.GetFastestTime(gameState);
            Console.WriteLine($"Fastest Time: {fastestTime:f2}");
            Console.WriteLine();

            Strat fastestStrat = RecursivePlayer.GetFastestStrat(gameState);
            RecursivePlayer.PlayStrat(gameState, fastestStrat);

            gameState = new(startingConditions);
            StratSet allStrats = RecursivePlayer.GetAllStrats(gameState);
            allStrats.Sort();
            allStrats.Print();
            allStrats.PrintSummaries();
            */
        }
    }
}