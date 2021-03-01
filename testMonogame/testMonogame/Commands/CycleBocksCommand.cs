using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleBocksCommand : ICommand
    {
        Game1 game;
        int incDec;
        public CycleBocksCommand(Game1 game, int incDecIn)
        {
            this.game = game;
            incDec = incDecIn;
        }

        public void Execute()
        {
            game.cycleBlock(incDec);
        }
    }
}
