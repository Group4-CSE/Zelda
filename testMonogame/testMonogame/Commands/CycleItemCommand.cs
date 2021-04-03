using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleItemCommand : ICommand
    {
        GameManager game;
        int incDec;
        public CycleItemCommand(GameManager game, int incDecIn)
        {
            this.game = game;
            incDec= incDecIn;
        }

        public void Execute()
        {
            //game.cycleItem(incDec);
        }
    }
}
