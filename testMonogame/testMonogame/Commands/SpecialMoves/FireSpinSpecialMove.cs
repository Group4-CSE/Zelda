using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands.SpecialMoves
{
    class FireSpinSpecialMove:ICommand
    {
        GameManager game;
        public FireSpinSpecialMove(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            game.getPlayer().fireSpin(game);
        }
    }
}
