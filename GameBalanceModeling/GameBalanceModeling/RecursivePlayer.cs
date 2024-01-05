namespace GameBalanceModeling
{
    public class RecursivePlayer
    {
        public static void PlayStrat(GameState gameState, Strat strat)
        {
            Console.WriteLine($"Playing the {strat.Time:f2} strategy:");
            for (int i = strat.Commands.Count - 1; i >= 0; i--)
            {
                ICommand command = strat.Commands[i].CreateCopy();
                Console.WriteLine($"L {gameState.Level}: {command.Name}");
                command.Do(gameState);
            }

            Console.WriteLine($"L {gameState.Level} in {gameState.Time:f2}");
            Console.WriteLine();
        }

        public static double GetFastestTime(GameState gameState)
        {
            if (gameState.IsVictory) return gameState.Time;

            double minTime = double.MaxValue;

            foreach (ICommand command in gameState.Commands)
            {
                ICommand newCommand = command.CreateCopy();

                if (newCommand.IsAvailable(gameState))
                {
                    newCommand.Do(gameState);

                    double currentTime = GetFastestTime(gameState);
                    if (currentTime < minTime)
                    {
                        minTime = currentTime;
                    }

                    newCommand.Undo(gameState);
                }
            }

            return minTime;
        }

        public static Strat GetFastestStrat(GameState gameState)
        {
            if (gameState.IsVictory) return new Strat(gameState.Time);

            double minTime = double.MaxValue;
            Strat fastestStrat = new(minTime);

            foreach (ICommand possibleCommand in gameState.Commands)
            {
                ICommand currentCommand = possibleCommand.CreateCopy();

                if (currentCommand.IsAvailable(gameState))
                {
                    currentCommand.Do(gameState);

                    Strat currentStrat = GetFastestStrat(gameState);
                    if (currentStrat.Time < minTime)
                    {
                        minTime = currentStrat.Time;
                        fastestStrat = currentStrat;
                        fastestStrat.Commands.Add(currentCommand);
                    }

                    currentCommand.Undo(gameState);
                }
            }

            return fastestStrat;
        }

        public static StratSet GetAllStrats(GameState gameState)
        {
            if (gameState.IsVictory) return [new Strat(gameState.Time)];

            StratSet allStrats = [];

            foreach (ICommand possibleCommand in gameState.Commands)
            {
                ICommand currentCommand = possibleCommand.CreateCopy();

                if (currentCommand.IsAvailable(gameState))
                {
                    currentCommand.Do(gameState);

                    StratSet currentStrats = GetAllStrats(gameState);
                    if (currentStrats.Count > 0)
                    {
                        foreach (Strat strat in currentStrats)
                        {
                            strat.Commands.Add(currentCommand);
                        }

                        allStrats.AddRange(currentStrats);
                    }

                    currentCommand.Undo(gameState);
                }
            }

            return allStrats;
        }
    }
}