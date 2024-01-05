namespace GameBalanceModeling
{
    public class DoubleIncome : ICommand
    {
        private GameplayData _gameplayData;

        public string Name => "DoubleIncome";

        public ICommand CreateCopy() => new DoubleIncome();

        public bool IsAvailable(GameState gameState) => !gameState.IsIncomeDoubled;

        public void Do(GameState gameState)
        {
            _gameplayData = gameState.GameplayData;
            gameState.DoubleIncome();
        }

        public void Undo(GameState gameState)
        {
            gameState.GameplayData = _gameplayData;
        }
    }
}