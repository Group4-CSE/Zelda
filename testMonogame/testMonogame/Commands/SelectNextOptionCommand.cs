using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class SelectNextOptionCommand : ICommand
    {
        GameManager game;
        public SelectNextOptionCommand(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            //selects the next start menu box
            game.NextStartMenuBox();
        }
    }
}
