using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleItemCommand : ICommand
    {
        Game1 game;
        int incDec;
        public CycleItemCommand(Game1 game, int incDecIn)
        {
            this.game = game;
            incDec= incDecIn;
        }

        public void Execute()
        {
            game.cycleItem(incDec);
        }
    }
}
