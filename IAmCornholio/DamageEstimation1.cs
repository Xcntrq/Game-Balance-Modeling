namespace DamageEstimation
{
    internal class Program
    {
        static void Main()
        {
            for (int x2AtLvl = 1; x2AtLvl <= 16; x2AtLvl++)
            {
                decimal totalDmg = 0;
                decimal totalTime = 0;
                decimal x2Price = 9000;
                decimal currentDmg = 1000;
                decimal nextLvlPrice = 1000;
                for (int currentLvl = 1; currentLvl <= 15; currentLvl++)
                {
                    decimal goldPerSec = currentDmg * 0.05m;

                    if (currentLvl == x2AtLvl)
                    {
                        decimal timeToX2 = x2Price / goldPerSec;
                        totalDmg += timeToX2 * currentDmg;
                        totalTime += timeToX2;
                        currentDmg *= 4m;
                        goldPerSec = currentDmg * 0.05m;
                    }

                    decimal timeToNextLvl = nextLvlPrice / goldPerSec;
                    totalDmg += timeToNextLvl * currentDmg;
                    totalTime += timeToNextLvl;

                    // Lvl up
                    currentDmg *= 1.2m;
                    nextLvlPrice *= 1.6m;
                }

                Console.WriteLine($"If x2 at {x2AtLvl}: Lvl 15 total dmg = {totalDmg:f1} in {totalTime:f1} sec");
            }
        }
    }
}