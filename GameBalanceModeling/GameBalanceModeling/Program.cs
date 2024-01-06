namespace GameBalanceModeling
{
    public class Program
    {
        static void Main()
        {
            StartingConditions startingConditions = new();

            double maxTimeDifference = 0;
            double relativeDifference = 0;
            double bestTimePossible = double.MaxValue;

            /*
            startingConditions.LevelUpCost = 711;
            startingConditions.IncomePerSec = 6;
            startingConditions.DoubleIncomeCost = 12138;
            startingConditions.LevelUpCostFactor = 9.2;
            startingConditions.IncomePerSecFactor = 3.8;

            GameState gameState = new(startingConditions);
            StratSet allStrats = RecursivePlayer.GetAllStrats(gameState);
            allStrats.Sort();

            gameState = new(startingConditions);
            RecursivePlayer.PlayStrat(gameState, allStrats[0]);
            */

            while (startingConditions.Next())
            {
                GameState gameState = new(startingConditions);
                StratSet allStrats = RecursivePlayer.GetAllStrats(gameState);
                allStrats.Sort();

                double timeDifference = allStrats[1].Time - allStrats[0].Time;

                bool areConditionsOkay = startingConditions.LevelUpCost > startingConditions.IncomePerSec * 10;
                bool isTimeOkay = (allStrats[0].Time < 1800) && (allStrats[0].Time > 300);
                bool isSavingMuch = (timeDifference / allStrats[1].Time) > relativeDifference;
                bool greaterTimeDifference = timeDifference > maxTimeDifference;
                bool smallerOverallTime = (timeDifference == maxTimeDifference) && (allStrats[0].Time < bestTimePossible);
                bool prefferedTimeDifference = allStrats[0].HasDoubleIncomeInTheMiddle && isTimeOkay && isSavingMuch && areConditionsOkay;

                if (prefferedTimeDifference)
                {
                    relativeDifference = timeDifference / allStrats[1].Time;
                    bestTimePossible = allStrats[0].Time;
                    maxTimeDifference = timeDifference;
                    Console.WriteLine($"relativeDifference {relativeDifference:f2}:");
                    Console.WriteLine($"timeDifference {timeDifference:f2}:");
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