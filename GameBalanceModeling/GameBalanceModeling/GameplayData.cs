namespace GameBalanceModeling
{
    public struct GameplayData(StartingConditions startingConditions)
    {
        public bool IsIncomeDoubled = false;
        public double Time = 0;
        public int Level = 1;

        public int LevelUpCost = startingConditions.LevelUpCost;
        public int IncomePerSec = startingConditions.IncomePerSec;
        public int DoubleIncomeCost = startingConditions.DoubleIncomeCost;
        public double LevelUpCostFactor = startingConditions.LevelUpCostFactor;
        public double IncomePerSecFactor = startingConditions.IncomePerSecFactor;

        public void LevelUp()
        {
            Level++;
            Time += (double)LevelUpCost / IncomePerSec;
            IncomePerSec = (int)(IncomePerSec * IncomePerSecFactor);
            LevelUpCost = (int)(LevelUpCost * LevelUpCostFactor);
        }

        public void DoubleIncome()
        {
            IsIncomeDoubled = true;
            Time += (double)DoubleIncomeCost / IncomePerSec;
            IncomePerSec *= 2;
        }
    }
}