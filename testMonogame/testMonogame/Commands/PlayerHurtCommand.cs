using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerHurtCommand : ICommand
    {
        IPlayer player;
        GameManager game;
        public PlayerHurtCommand(IPlayer playerIn, GameManager gameIn)
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
