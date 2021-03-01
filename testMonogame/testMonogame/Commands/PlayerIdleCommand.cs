using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class PlayerIdleCommand : ICommand
    {
        IPlayer player;
        public PlayerIdleCommand(IPlayer playerIn)
        {
            player = playerIn;
        }
        public void Execute()
        {
            player.getState().setMoving(false);
        }
    }
}
