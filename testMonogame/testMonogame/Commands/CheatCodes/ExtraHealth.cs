using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ExtraHealth : ICommand
    {
        IPlayer player;

        public ExtraHealth(IPlayer playerIn)
        {
            player = playerIn;

        }
        public void Execute()
        {
            player.maxHealth = player.maxHealth * 2;
            player.health = player.maxHealth;
        }
    }
}
