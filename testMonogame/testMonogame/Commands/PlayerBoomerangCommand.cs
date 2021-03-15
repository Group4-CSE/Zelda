using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerBoomerangCommand : ICommand
    {
        IPlayer player;
        GameManager game;
        public PlayerBoomerangCommand(IPlayer playerIn, GameManager gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.UseBoomerang(game);
        }
    }
}
