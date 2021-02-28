using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerHurtCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        public PlayerHurtCommand(IPlayer playerIn, Game1 gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.TakeDamage(1);
        }
    }
}
