using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class UnlimitedBombs : ICommand
    {
        IPlayer player;
        GameManager game;
        public UnlimitedBombs(IPlayer playerIn, GameManager gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.Bombs = 10000;
        }
    }
}
