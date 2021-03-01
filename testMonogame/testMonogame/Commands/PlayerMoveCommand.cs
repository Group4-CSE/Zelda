using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerMoveCommand : ICommand
    {
        IPlayer player;
        public PlayerMoveCommand(IPlayer playerIn)
        {
            player = playerIn;
        }
        public void Execute()
        {
            player.getState().setMoving(true);
        }
    }
}
