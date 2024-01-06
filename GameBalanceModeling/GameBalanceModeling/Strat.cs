namespace GameBalanceModeling
{
    using System.Text;

    public class Strat(double time)
    {
        public double Time = time;
        public List<ICommand> Commands = [];

        public bool HasDoubleIncome
        {
            get
            {
                for (int i = Commands.Count - 1; i > 0; i--)
                {
                    if (Commands[i] is DoubleIncome) return true;
                }

                return false;
            }
        }

        public bool HasDoubleIncomeInTheMiddle
        {
            get
            {
                for (int i = Commands.Count - 3; i > 2; i--)
                {
                    if (Commands[i] is DoubleIncome) return true;
                }

                return false;
            }
        }

        public bool HasDoubleIncomeGoFirst => Commands[^1] is DoubleIncome;

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
}