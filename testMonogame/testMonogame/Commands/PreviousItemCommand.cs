using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class PreviousItemCommand : ICommand
    {
        IPlayer p;
        public PreviousItemCommand(GameManager g)
        {
            p = g.getPlayer();
        }
        public void Execute()
        {
            p.PreviousItem();
        }
    }
}
