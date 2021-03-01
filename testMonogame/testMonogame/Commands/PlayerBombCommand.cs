using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerBombCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        public PlayerBombCommand(IPlayer playerIn, Game1 gameIn)
        {
            player = playerIn;
            game = gameIn;
        }
        public void Execute()
        {
            player.UseBomb(game);
        }
    }
}
