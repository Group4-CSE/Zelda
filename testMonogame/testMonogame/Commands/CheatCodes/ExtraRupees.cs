using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ExtraRupees : ICommand
    {
        IPlayer player;

        public ExtraRupees(IPlayer playerIn)
        {
            player = playerIn;

        }
        public void Execute()
        {
            player.Rupees = player.Rupees+ 25;
        }
    }
}
