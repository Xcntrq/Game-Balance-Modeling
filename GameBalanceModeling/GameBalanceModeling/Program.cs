using System.Text;

namespace GameBalanceModeling
{
    public class Program
    {
        static void Main()
        {
            GameState gameState = new();
            double fastestTime = RecursivePlayer.GetFastestTime(gameState);
            Console.WriteLine($"Fastest Time: {fastestTime:f2}");
            Console.WriteLine();

            Strat fastestStrat = RecursivePlayer.GetFastestStrat(gameState);
            RecursivePlayer.PlayStrat(gameState, fastestStrat);

            gameState = new();
            StratSet allStrats = RecursivePlayer.GetAllStrats(gameState);
            allStrats.Sort();
            allStrats.Print();
            allStrats.PrintSummaries();
        }
    }

    public class StratSet : List<Strat>
    {
        public void Print()
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].PrintOneLine();
            }

            Console.WriteLine();
        }

        public void PrintSummaries()
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].PrintSummary();
            }

            Console.WriteLine();
        }

        public new void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                int minJ = 0;
                double minTime = double.MaxValue;

                for (int j = i; j < Count; j++)
                {
                    if (this[j].Time < minTime)
                    {
                        minJ = j;
                        minTime = this[j].Time;
                    }
                }

                (this[i], this[minJ]) = (this[minJ], this[i]);
            }
        }
    }

    public class Strat
    {
        public double Time;
        public List<ICommand> Commands = new();

        public Strat(double time) => Time = time;

        public void PrintOneLine()
        {
            StringBuilder sb = new($"{Time:f2}: ");

            for (int i = Commands.Count - 1; i > 0; i--)
            {
                sb.Append(Commands[i].Name);
                sb.Append(" -> ");
            }

            sb.Append(Commands[0].Name);
            Console.WriteLine(sb);
        }

        public void PrintSummary()
        {
            int i;
            bool isDoubled = false;
            for (i = Commands.Count - 1; i >= 0; i--)
            {
                if (Commands[i] is DoubleIncome)
                {
                    isDoubled = true;
                    break;
                }
            }

            Console.WriteLine(isDoubled ? $"{Time:f2}: x2 at {Commands.Count - i}" : $"{Time:f2}: x2 never");
        }
    }

    public class RecursivePlayer
    {
        public static void PlayStrat(GameState gameState, Strat strat)
        {
            Console.WriteLine($"Playing the {strat.Time:f2} strategy:");
            for (int i = strat.Commands.Count - 1; i >= 0; i--)
            {
                ICommand command = strat.Commands[i].CreateCopy(gameState);
                Console.WriteLine($"L {gameState.CurrentLevel}: {command.Name}");
                command.Do();
            }

            Console.WriteLine($"L {gameState.CurrentLevel} in {gameState.Time:f2}");
            Console.WriteLine();
        }

        public static double GetFastestTime(GameState gameState)
        {
            if (gameState.IsVictory) return gameState.Time;

            double minTime = double.MaxValue;

            foreach (ICommand possibleCommand in gameState.PossibleCommands)
            {
                ICommand currentCommand = possibleCommand.CreateCopy(gameState);

                if (currentCommand.IsAvailable)
                {
                    currentCommand.Do();

                    double currentTime = GetFastestTime(gameState);
                    if (currentTime < minTime)
                    {
                        minTime = currentTime;
                    }

                    currentCommand.Undo();
                }
            }

            return minTime;
        }

        public static Strat GetFastestStrat(GameState gameState)
        {
            if (gameState.IsVictory) return new Strat(gameState.Time);

            double minTime = double.MaxValue;
            Strat fastestStrat = new(minTime);

            foreach (ICommand possibleCommand in gameState.PossibleCommands)
            {
                ICommand currentCommand = possibleCommand.CreateCopy(gameState);

                if (currentCommand.IsAvailable)
                {
                    currentCommand.Do();

                    Strat currentStrat = GetFastestStrat(gameState);
                    if (currentStrat.Time < minTime)
                    {
                        minTime = currentStrat.Time;
                        fastestStrat = currentStrat;
                        fastestStrat.Commands.Add(currentCommand);
                    }

                    currentCommand.Undo();
                }
            }

            return fastestStrat;
        }

        public static StratSet GetAllStrats(GameState gameState)
        {
            if (gameState.IsVictory) return new StratSet() { new Strat(gameState.Time) };

            StratSet allStrats = new();

            foreach (ICommand possibleCommand in gameState.PossibleCommands)
            {
                ICommand currentCommand = possibleCommand.CreateCopy(gameState);

                if (currentCommand.IsAvailable)
                {
                    currentCommand.Do();

                    StratSet currentStrats = GetAllStrats(gameState);
                    if (currentStrats.Count > 0)
                    {
                        foreach (Strat strat in currentStrats)
                        {
                            strat.Commands.Add(currentCommand);
                        }

                        allStrats.AddRange(currentStrats);
                    }

                    currentCommand.Undo();
                }
            }

            return allStrats;
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

        public List<ICommand> PossibleCommands;

        public bool IsVictory => CurrentLevel >= 10;

        public GameState()
        {
            IncomePerSecond = 1000;
            CurrentLevel = 1;
            LevelUpCost = 1000;
            DoubleIncomeCost = 9000;
            IsIncomeDoubled = false;
            Time = 0;

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
        public string Name { get; }
        public ICommand CreateCopy(GameState gameState);
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

        public bool IsAvailable => true;

        public string Name => "LevelUp";

        public LevelUp(GameState gameState) => _gameState = gameState;

        public ICommand CreateCopy(GameState gameState) => new LevelUp(gameState);

        public void Do()
        {
            _incomePerSecond = _gameState.IncomePerSecond;
            _currentLevel = _gameState.CurrentLevel;
            _levelUpCost = _gameState.LevelUpCost;
            _time = _gameState.Time;

            _gameState.LevelUp();
        }

        public void Undo()
        {
            _gameState.IncomePerSecond = _incomePerSecond;
            _gameState.CurrentLevel = _currentLevel;
            _gameState.LevelUpCost = _levelUpCost;
            _gameState.Time = _time;
        }
    }

    public class DoubleIncome : ICommand
    {
        private readonly GameState _gameState;
        private int _incomePerSecond;
        private bool _isIncomeDoubled;
        private double _time;

        public bool IsAvailable => !_gameState.IsIncomeDoubled;

        public string Name => "DoubleIncome";

        public DoubleIncome(GameState gameState) => _gameState = gameState;

        public ICommand CreateCopy(GameState gameState) => new DoubleIncome(gameState);

        public void Do()
        {
            _incomePerSecond = _gameState.IncomePerSecond;
            _isIncomeDoubled = _gameState.IsIncomeDoubled;
            _time = _gameState.Time;

            _gameState.DoubleIncome();
        }

        public void Undo()
        {
            _gameState.IncomePerSecond = _incomePerSecond;
            _gameState.IsIncomeDoubled = _isIncomeDoubled;
            _gameState.Time = _time;
        }
    }
}