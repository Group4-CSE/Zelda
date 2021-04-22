using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class Invincibility : ICommand
    {
        IPlayer player;
        GameManager game;
        public Invincibility(IPlayer playerIn, GameManager gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.invincible = true;
        }
    }
}
