using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class UnlimitedBombs : ICommand
    {
        IPlayer player;

        public UnlimitedBombs(IPlayer playerIn)
        {
            player = playerIn;

        }
        public void Execute()
        {
            player.Bombs = 10000;
        }
    }
}
