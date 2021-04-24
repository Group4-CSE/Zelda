using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class SelectPreviousOptionCommand : ICommand
    {
        GameManager game;
        public SelectPreviousOptionCommand(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            //selects the prev start menu box
            game.PreviouStartMenuBox();
        }
    }
}
