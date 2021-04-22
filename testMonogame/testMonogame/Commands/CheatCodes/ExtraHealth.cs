using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ExtraHealth : ICommand
    {
        IPlayer player;
        GameManager game;
        public ExtraHealth(IPlayer playerIn, GameManager gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.maxHealth = player.maxHealth * 2;
            player.health = player.maxHealth;
        }
    }
}
