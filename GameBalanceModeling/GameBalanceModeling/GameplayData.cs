namespace GameBalanceModeling
{
    public struct GameplayData(StartingConditions startingConditions)
    {
        public bool IsIncomeDoubled = false;
        public double Time = 0;
        public long Level = 1;

        public long LevelUpCost = startingConditions.LevelUpCost;
        public long IncomePerSec = startingConditions.IncomePerSec;
        public long DoubleIncomeCost = startingConditions.DoubleIncomeCost;
        public double LevelUpCostFactor = startingConditions.LevelUpCostFactor;
        public double IncomePerSecFactor = startingConditions.IncomePerSecFactor;

        public void LevelUp()
        {
            Level++;
            Time += (double)LevelUpCost / IncomePerSec;
            IncomePerSec = (long)(IncomePerSec * IncomePerSecFactor);
            LevelUpCost = (long)(LevelUpCost * LevelUpCostFactor);
        }

        public void DoubleIncome()
        {
            IsIncomeDoubled = true;
            Time += (double)DoubleIncomeCost / IncomePerSec;
            IncomePerSec *= 2;
        }
    }
}