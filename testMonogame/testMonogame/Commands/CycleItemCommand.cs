using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleItemCommand : ICommand
    {
        Game1 game;
        public CycleItemCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.cycleItem();
        }
    }
}
