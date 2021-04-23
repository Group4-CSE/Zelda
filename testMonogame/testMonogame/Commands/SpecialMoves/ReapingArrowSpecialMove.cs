using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands.SpecialMoves
{
    class ReapingArrowSpecialMove : ICommand
    {
        GameManager game;
        public ReapingArrowSpecialMove(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            game.getPlayer().UseReapingArrow(game);
        }
    }
}
