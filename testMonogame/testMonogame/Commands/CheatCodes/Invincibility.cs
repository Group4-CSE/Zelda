using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class Invincibility : ICommand
    {
        IPlayer player;

        public Invincibility(IPlayer playerIn)
        {
            player = playerIn;

        }
        public void Execute()
        {
            player.invincible = true;
        }
    }
}
