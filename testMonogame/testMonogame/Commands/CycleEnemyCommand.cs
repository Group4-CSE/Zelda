using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleEnemyCommand : ICommand
    {
        Game1 game;
        int incDec;
        public CycleEnemyCommand(Game1 game, int incDecIn)
        {
            this.game = game;
            incDec = incDecIn;
        }

        public void Execute()
        {
            game.cycleEnemy(incDec);
        }
    }
}
