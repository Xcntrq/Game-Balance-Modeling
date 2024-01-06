namespace GameBalanceModeling
{
    public class GameState(StartingConditions startingConditions)
    {
        public GameplayData GameplayData = new(startingConditions);
        public List<ICommand> Commands = [new LevelUp(), new DoubleIncome()];

        public bool IsVictory => GameplayData.Level >= 10;
        public bool IsIncomeDoubled => GameplayData.IsIncomeDoubled;
        public long Level => GameplayData.Level;
        public double Time => GameplayData.Time;

        public void LevelUp() => GameplayData.LevelUp();

        public void DoubleIncome() => GameplayData.DoubleIncome();
    }
}