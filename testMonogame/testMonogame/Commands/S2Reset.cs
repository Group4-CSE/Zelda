using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class S2Reset : ICommand
    {
        Game1 game;
        public S2Reset(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.s2reset();
        }
    }
}
