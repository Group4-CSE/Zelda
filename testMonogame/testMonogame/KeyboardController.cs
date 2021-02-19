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

        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            //run the command associated with any key pressed.
            foreach (Keys k in state.GetPressedKeys())
            {
                //only attempt to execute if the key is present in the dictionary
                if (KeyMap.ContainsKey(k)&&!prevState.IsKeyDown(k)) KeyMap[k].Execute();
            }

            prevState = state;
        }

    }
}
