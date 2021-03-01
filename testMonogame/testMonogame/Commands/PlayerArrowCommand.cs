using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerArrowCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        public PlayerArrowCommand(IPlayer playerIn, Game1 gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.UseBow(game);
        }
    }
}
