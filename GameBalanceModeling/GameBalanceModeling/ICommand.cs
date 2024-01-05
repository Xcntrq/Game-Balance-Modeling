namespace GameBalanceModeling
{
    public interface ICommand
    {
        public string Name { get; }
        public ICommand CreateCopy();
        public bool IsAvailable(GameState gameState);
        public void Do(GameState gameState);
        public void Undo(GameState gameState);
    }
}