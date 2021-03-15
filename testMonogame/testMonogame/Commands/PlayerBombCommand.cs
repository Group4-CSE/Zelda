using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerBombCommand : ICommand
    {
        IPlayer player;
        GameManager game;
        public PlayerBombCommand(IPlayer playerIn, GameManager gameIn)
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
