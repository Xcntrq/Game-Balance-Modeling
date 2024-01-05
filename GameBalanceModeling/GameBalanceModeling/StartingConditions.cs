namespace GameBalanceModeling
{
    public class StartingConditions
    {
        public int LevelUpCost;
        public int IncomePerSec;
        public int DoubleIncomeCost;
        public double LevelUpCostFactor;
        public double IncomePerSecFactor;

        public StartingConditions()
        {
            LevelUpCost = 1000;
            IncomePerSec = 1000;
            DoubleIncomeCost = 9000;
            LevelUpCostFactor = 1.6;
            IncomePerSecFactor = 1.2;
        }
    }
}