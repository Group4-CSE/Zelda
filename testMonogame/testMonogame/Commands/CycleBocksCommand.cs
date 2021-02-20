using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleBocksCommand : ICommand
    {
        Game1 game;
        public CycleBocksCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.cycleBlock();
        }
    }
}
