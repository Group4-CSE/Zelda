using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame.Commands
{
    public class QuitCommand : ICommand
    {
        GameManager game;
        public QuitCommand(GameManager game)
        {
            this.game = game;
        }

        public void Execute()
        {
            //quit logic
            game.Exit();
        }
    }
}
