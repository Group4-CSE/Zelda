using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class S2Reset : ICommand
    {
        GameManager game;
        public S2Reset(GameManager game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.s2reset();
        }
    }
}
