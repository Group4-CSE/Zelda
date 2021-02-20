using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace testMonogame
{
    public class KeyboardController : IController
    {
        Dictionary<Keys, ICommand> KeyMap;
        KeyboardState prevState;

        public KeyboardController(Game1 game)
        {
            prevState = Keyboard.GetState();

            //setup commands
            ICommand quit = new Commands.QuitCommand(game);
            ICommand block = new Commands.CycleBocksCommand(game);
            ICommand item = new Commands.CycleItemCommand(game);
            ICommand enemy = new Commands.CycleEnemyCommand(game);
            ICommand s2reset = new Commands.S2Reset(game);


            //set up the dictionary
            KeyMap = new Dictionary<Keys, ICommand>();

            KeyMap.Add(Keys.Escape, quit);
            KeyMap.Add(Keys.T, block);
            KeyMap.Add(Keys.Y, block);
            KeyMap.Add(Keys.U, item);
            KeyMap.Add(Keys.I, item);
            KeyMap.Add(Keys.O, enemy);
            KeyMap.Add(Keys.P, enemy);
            KeyMap.Add(Keys.R, s2reset);

        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //run the command associated with any key pressed.
            foreach (Keys k in state.GetPressedKeys())
            {
                //only attempt to execute if the key is present in the dictionary and has been pressed
                if (KeyMap.ContainsKey(k)&&!prevState.IsKeyDown(k)) KeyMap[k].Execute();
            }

            prevState = state;
        }

    }
}
