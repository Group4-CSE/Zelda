using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleBocksCommand : ICommand
    {
        GameManager game;
        int incDec;
        public CycleBocksCommand(GameManager game, int incDecIn)
        {
            this.game = game;
            incDec = incDecIn;
        }

        public void Execute()
        {
            //game.cycleBlock(incDec);
        }
    }
}
