using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerAttackCommand : ICommand
    {
        IPlayer player;
        GameManager game;
        public PlayerAttackCommand(IPlayer playerIn, GameManager gameIn)
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
