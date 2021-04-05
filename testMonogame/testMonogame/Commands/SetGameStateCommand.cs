using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class SetGameStateCommand : ICommand
    {
        GameManager game;
        int state;
        public SetGameStateCommand(GameManager ingame, int inState)
        {
            game = ingame;
            state = inState;
        }
        public void Execute()
        {
            game.SetState(state);
        }
    }
}
