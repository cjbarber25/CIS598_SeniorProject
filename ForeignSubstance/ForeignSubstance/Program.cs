using System;

namespace ForeignSubstance
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ForeignSubstance())
                game.Run();
        }
    }
}
