namespace GameBalanceModeling
{
    public class Program
    {
        static void Main()
        {
            StartingConditions startingConditions = new();
            GameState gameState = new(startingConditions);
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
        }
    }
}