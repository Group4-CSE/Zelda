using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands.SpecialMoves
{
    class RupeeShieldSpecialMove : ICommand
    {
        GameManager game;
        public RupeeShieldSpecialMove(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            game.getPlayer().PlaceRupeeShield(game);
        }
    }
}
