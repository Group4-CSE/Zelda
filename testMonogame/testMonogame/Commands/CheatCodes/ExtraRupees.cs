using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ExtraRupees : ICommand
    {
        IPlayer player;
        GameManager game;
        public ExtraRupees(IPlayer playerIn, GameManager gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.rupees = 25;
        }
    }
}
