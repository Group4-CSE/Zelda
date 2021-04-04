using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class NextItemCommand : ICommand
    {
        IPlayer p;
        public NextItemCommand(GameManager g)
        {
            p = g.getPlayer();
        }
        public void Execute()
        {
            p.NextItem();
        }
    }
}
