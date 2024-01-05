namespace GameBalanceModeling
{
    public class LevelUp : ICommand
    {
        private GameplayData _gameplayData;

        public string Name => "LevelUp";

        public ICommand CreateCopy() => new LevelUp();

        public bool IsAvailable(GameState gameState) => !gameState.IsVictory;

        public void Do(GameState gameState)
        {
            _gameplayData = gameState.GameplayData;
            gameState.LevelUp();
        }

        public void Undo(GameState gameState)
        {
            gameState.GameplayData = _gameplayData;
        }
    }
}