using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerAttackCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        public PlayerAttackCommand(IPlayer playerIn, Game1 gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.Attack(game);
        }
    }
}
