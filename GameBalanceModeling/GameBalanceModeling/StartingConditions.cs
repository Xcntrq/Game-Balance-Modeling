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
            LevelUpCost = 5;
            IncomePerSec = 10;
            DoubleIncomeCost = 10;
            LevelUpCostFactor = 1.1;
            IncomePerSecFactor = 1.1;
        }

        public void Print()
        {
            Console.WriteLine($"LevelUpCost: {LevelUpCost}");
            Console.WriteLine($"IncomePerSec: {IncomePerSec}");
            Console.WriteLine($"DoubleIncomeCost: {DoubleIncomeCost}");
            Console.WriteLine($"LevelUpCostFactor: {LevelUpCostFactor:f2}");
            Console.WriteLine($"IncomePerSecFactor: {IncomePerSecFactor:f2}");
        }

        public bool Next()
        {
            if (LevelUpCost < 10000)
            {
                LevelUpCost *= 2;
                return true;
            }

            if (IncomePerSec < 10000)
            {
                LevelUpCost = 10;
                IncomePerSec *= 2;
                return true;
            }

            if (DoubleIncomeCost < 10000)
            {
                LevelUpCost = 10;
                IncomePerSec = 10;
                DoubleIncomeCost *= 2;
                return true;
            }

            if (LevelUpCostFactor < 13)
            {
                LevelUpCost = 10;
                IncomePerSec = 10;
                DoubleIncomeCost = 10;
                LevelUpCostFactor += 0.3;
                return true;
            }

            if (IncomePerSecFactor < 13)
            {
                LevelUpCost = 10;
                IncomePerSec = 10;
                DoubleIncomeCost = 10;
                LevelUpCostFactor = 0.1;
                IncomePerSecFactor += 0.3;
                return true;
            }

            return false;
        }
    }
}