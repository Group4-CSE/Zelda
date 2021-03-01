using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerBoomerangCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        public PlayerBoomerangCommand(IPlayer playerIn, Game1 gameIn)
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
