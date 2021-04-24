using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class NextOptionBasedOnSelectionCommand : ICommand
    {
        GameManager game;
        public NextOptionBasedOnSelectionCommand(GameManager g)
        {
            game = g;
        }
        public void Execute()
        {
            int selected = game.GetStartMenuSelectedBox();
            //once we have the selected box we can react accordingly
            //if we have the difficulty box selected we go to the next hardest difficulty(with wraparound)
            //if we have the mode box selected we will toggle
            //if we have the play box selected we will start the game

            if (selected == 0)
            {
                //difficulty
                game.ChangeDifficulty(1);
            }
            else if (selected == 1)
            {
                //mode
                if (game.IsHorde()) game.SetHorde(false);
                else game.SetHorde(true);
            }
            else if (selected == 2)
            {
                //play
                game.SetState(0); //0=playing
            }
        }
    }
}
