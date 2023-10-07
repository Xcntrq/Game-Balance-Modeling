namespace GameBalanceModeling
{
    public class Program
    {
        static void Main()
        {
            GameState gameState = new();
            RecursivePlayer recursivePlayer = new();
            double fastestTime = recursivePlayer.GetFastestTime(gameState);
            Console.WriteLine(fastestTime);
        }
    }

    public class Strat
    {
        public List<ICommand> Commands = new();
    }

    public class RecursivePlayer
    {
        public double GetFastestTime(GameState gameState)
        {
            if (gameState.IsVictory) return gameState.Time;

            double minTime = double.MaxValue;

            foreach (ICommand possibleCommand in gameState.PossibleCommands)
            {
                if (possibleCommand.IsAvailable)
                {
                    possibleCommand.Do();

                    double currentTime = GetFastestTime(gameState);
                    if (currentTime < minTime)
                    {
                        minTime = currentTime;
                    }

                    possibleCommand.Undo();
                }
            }

            return minTime;
        }
    }

    public class GameState
    {
        public int IncomePerSecond;
        public int CurrentLevel;
        public int LevelUpCost;
        public int DoubleIncomeCost;
        public bool IsIncomeDoubled;
        public double Time;

        public List<ICommand> PossibleCommands = new();

        public bool IsVictory => CurrentLevel >= 10;

        public GameState()
        {
            IncomePerSecond = 1000;
            CurrentLevel = 1;
            LevelUpCost = 1000;
            DoubleIncomeCost = 9000;
            IsIncomeDoubled = false;
            Time = 0;
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            PossibleCommands = new()
            {
                new LevelUp(this),
                new DoubleIncome(this),
            };
        }

        public void LevelUp()
        {
            CurrentLevel++;
            Time += (double)LevelUpCost / IncomePerSecond;
            IncomePerSecond = (int)(IncomePerSecond * 1.2);
            LevelUpCost = (int)(LevelUpCost * 1.6);
        }

        public void DoubleIncome()
        {
            IsIncomeDoubled = true;
            Time += (double)DoubleIncomeCost / IncomePerSecond;
            IncomePerSecond *= 2;
        }
    }

    public interface ICommand
    {
        public bool IsAvailable { get; }
        public void Do();
        public void Undo();
    }

    public class LevelUp : ICommand
    {
        private readonly GameState _gameState;
        private int _incomePerSecond;
        private int _currentLevel;
        private int _levelUpCost;
        private double _time;

        private List<ICommand> _possibleCommands = new();

        public bool IsAvailable => true;

        public LevelUp(GameState gameState) => _gameState = gameState;

        public void Do()
        {
            _incomePerSecond = _gameState.IncomePerSecond;
            _currentLevel = _gameState.CurrentLevel;
            _levelUpCost = _gameState.LevelUpCost;
            _time = _gameState.Time;

            _possibleCommands = _gameState.PossibleCommands;
            _gameState.InitializeCommands();

            _gameState.LevelUp();
        }

        public void Undo()
        {
            _gameState.IncomePerSecond = _incomePerSecond;
            _gameState.CurrentLevel = _currentLevel;
            _gameState.LevelUpCost = _levelUpCost;
            _gameState.Time = _time;
            _gameState.PossibleCommands = _possibleCommands;
        }
    }

    public class DoubleIncome : ICommand
    {
        private readonly GameState _gameState;
        private int _incomePerSecond;
        private bool _isIncomeDoubled;
        private double _time;

        private List<ICommand> _possibleCommands = new();

        public bool IsAvailable => !_gameState.IsIncomeDoubled;

        public DoubleIncome(GameState gameState) => _gameState = gameState;

        public void Do()
        {
            _incomePerSecond = _gameState.IncomePerSecond;
            _isIncomeDoubled = _gameState.IsIncomeDoubled;
            _time = _gameState.Time;

            _possibleCommands = _gameState.PossibleCommands;
            _gameState.InitializeCommands();

            _gameState.DoubleIncome();
        }

        public void Undo()
        {
            _gameState.IncomePerSecond = _incomePerSecond;
            _gameState.IsIncomeDoubled = _isIncomeDoubled;
            _gameState.Time = _time;
            _gameState.PossibleCommands = _possibleCommands;
        }
    }
}