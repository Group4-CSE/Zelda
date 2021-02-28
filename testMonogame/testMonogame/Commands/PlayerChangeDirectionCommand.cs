using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerChangeDirectionCommand : ICommand
    {
        IPlayer player;
        Game1 game;
        int direction;
        public PlayerChangeDirectionCommand(IPlayer playerIn, Game1 gameIn, int directionIn)
        {
            //0=up, 1=down, 2=right, 3=left
            player = playerIn;
            game = gameIn;
            direction = directionIn;
        }
        public void Execute()
        {
            player.ChangeState(direction);
        }
    }
}
