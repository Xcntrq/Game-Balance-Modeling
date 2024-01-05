namespace GameBalanceModeling
{
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
}