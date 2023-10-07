namespace DamageEstimation
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine($"Iteration: Success in {PlayIteratively(out int x2dpsAt):f3} sec, x2 at {x2dpsAt}");

            if (TryPlayRecursively(new GameState(), out decimal minTime, out x2dpsAt))
            {
                Console.WriteLine($"Recursion: Success in {minTime:f3} sec, x2 at {x2dpsAt}");
            }
            else
            {
                Console.WriteLine($"Recursion: Couldn't win {minTime:f3}");
            }
        }

        static decimal PlayIteratively(out int lvl)
        {
            int x2dpsAt = 0;
            decimal minTime = decimal.MaxValue;

            for (int x2AtLvl = 1; x2AtLvl <= 16; x2AtLvl++)
            {
                var gameState = new GameState();
                for (; !gameState.IsVictory;)
                {
                    if (gameState.CurrentLvl == x2AtLvl)
                    {
                        gameState.TryDoubleDps();
                    }

                    gameState.TryLvlUp();
                }

                if (gameState.TotalTime < minTime)
                {
                    x2dpsAt = x2AtLvl;
                    minTime = gameState.TotalTime;
                }
            }

            lvl = x2dpsAt;
            return minTime;
        }

        static bool TryPlayRecursively(GameState gameState, out decimal minTime, out int bestx2lvl)
        {
            minTime = decimal.MaxValue;
            bool isWinFound = false;
            bestx2lvl = 0;

            foreach (ICommand command in gameState.AllCommands)
            {
                if (command.TryExecute())
                {
                    if (TryPlayRecursively(gameState, out decimal time, out int x2lvl))
                    {
                        if (time < minTime)
                        {
                            minTime = time;
                            bestx2lvl = x2lvl;
                        }

                        isWinFound = true;
                    }

                    if (command is DoubleDps)
                    {
                        bestx2lvl = gameState.CurrentLvl;
                    }

                    command.Undo();
                }
            }

            if (gameState.IsVictory)
            {
                minTime = gameState.TotalTime;
                isWinFound = true;
            }

            return isWinFound;
        }
    }

    public class GameState
    {
        public int CurrentLvl;
        public decimal CurrentDps;
        public decimal NextLvlPrice;
        public decimal TotalTime;
        public decimal DpsMultiplier;
        public decimal NextLvlPriceMultiplier;
        public decimal DoubleDpsPrice;
        public bool IsDoubleDpsAvailable;

        public List<ICommand> AllCommands;

        public bool IsVictory => (CurrentLvl >= 15);

        public GameState()
        {
            CurrentLvl = 1;
            CurrentDps = 1000;
            NextLvlPrice = 1000;
            TotalTime = 0;
            DpsMultiplier = 1.2m;
            NextLvlPriceMultiplier = 1.6m;
            DoubleDpsPrice = 9000;
            IsDoubleDpsAvailable = true;
            AllCommands = new List<ICommand>();
            CreateCommands();
        }

        public void CreateCommands()
        {
            AllCommands.Add(new DoubleDps(this));
            AllCommands.Add(new LvlUp(this));
        }

        public bool TryLvlUp()
        {
            if (IsVictory)
            {
                return false;
            }

            TotalTime += NextLvlPrice / CurrentDps;
            NextLvlPrice *= NextLvlPriceMultiplier;
            CurrentDps *= DpsMultiplier;
            CurrentLvl++;

            return true;
        }

        public void LvlDown()
        {
            CurrentLvl--;
            CurrentDps /= DpsMultiplier;
            NextLvlPrice /= NextLvlPriceMultiplier;
            TotalTime -= NextLvlPrice / CurrentDps;
        }

        public bool TryDoubleDps()
        {
            if (!IsDoubleDpsAvailable)
            {
                return false;
            }

            IsDoubleDpsAvailable = false;
            TotalTime += DoubleDpsPrice / CurrentDps;
            CurrentDps *= 2;

            return true;
        }

        public void HalfDps()
        {
            CurrentDps /= 2;
            TotalTime -= DoubleDpsPrice / CurrentDps;
            IsDoubleDpsAvailable = true;
        }
    }

    public interface ICommand
    {
        public bool TryExecute();
        public void Undo();
    }

    public class LvlUp : ICommand
    {
        public GameState GameState;

        public LvlUp(GameState gameState)
        {
            GameState = gameState;
        }

        public bool TryExecute()
        {
            return GameState.TryLvlUp();
        }

        public void Undo()
        {
            GameState.LvlDown();
        }
    }

    public class DoubleDps : ICommand
    {
        public GameState GameState;

        public DoubleDps(GameState gameState)
        {
            GameState = gameState;
        }

        public bool TryExecute()
        {
            return GameState.TryDoubleDps();
        }

        public void Undo()
        {
            GameState.HalfDps();
        }
    }
}