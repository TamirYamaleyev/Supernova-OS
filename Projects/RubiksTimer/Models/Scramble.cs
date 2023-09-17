using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenterProject.Projects.RubiksTimer.Models
{
    class Scramble
    {
        private static readonly Random random = new Random();
        public static string GenerateScramble(int numberOfMoves)
        {
            StringBuilder scramble = new StringBuilder();
            string[] moves = { "U", "R", "F", "D", "L", "B" };

            for (int i = 0; i < numberOfMoves; i++)
            {
                int randomMoveIndex = random.Next(moves.Length);
                scramble.Append($"{moves[randomMoveIndex]}");

                // Double and Prime modifiers
                if (random.Next(3) == 0) 
                    scramble.Append(random.Next(2) == 0 ? "2" : "'");
                scramble.Append(" ");
            }
            return scramble.ToString().Trim();
        }
    }
}
