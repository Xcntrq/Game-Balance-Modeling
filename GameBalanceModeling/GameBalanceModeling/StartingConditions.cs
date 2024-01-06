namespace GameBalanceModeling
{
    public class StartingConditions
    {
        public long LevelUpCost;
        public long IncomePerSec;
        public long DoubleIncomeCost;
        public double LevelUpCostFactor;
        public double IncomePerSecFactor;

        public StartingConditions()
        {
            LevelUpCost = 2;
            IncomePerSec = 2;
            DoubleIncomeCost = 2;
            LevelUpCostFactor = 1;
            IncomePerSecFactor = 1;
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
                LevelUpCost = (long)(LevelUpCost * 1.5);
                return true;
            }

            if (IncomePerSec < 10000)
            {
                LevelUpCost = 2;
                IncomePerSec = (long)(IncomePerSec * 1.5);
                return true;
            }

            if (DoubleIncomeCost < 10000)
            {
                LevelUpCost = 2;
                IncomePerSec = 2;
                DoubleIncomeCost = (long)(DoubleIncomeCost * 1.5);
                return true;
            }

            if (LevelUpCostFactor < 13)
            {
                LevelUpCost = 2;
                IncomePerSec = 2;
                DoubleIncomeCost = 2;
                LevelUpCostFactor += 0.2;
                return true;
            }

            if (IncomePerSecFactor < 13)
            {
                LevelUpCost = 2;
                IncomePerSec = 2;
                DoubleIncomeCost = 2;
                LevelUpCostFactor = 1;
                IncomePerSecFactor += 0.2;
                return true;
            }

            return false;
        }
    }
}