using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerArrowCommand : ICommand
    {
        IPlayer player;
        GameManager game;
        public PlayerArrowCommand(IPlayer playerIn, GameManager gameIn)
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
