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

        ICommand Idle;
        ICommand Move;
        public KeyboardController(Game1 game)
        {
            prevState = Keyboard.GetState();


            //setup commands
            ICommand Attack = new PlayerAttackCommand(game.getPlayer(),game);
            Move = new PlayerMoveCommand(game.getPlayer());
            Idle = new PlayerIdleCommand(game.getPlayer());
            ICommand Arrow = new PlayerArrowCommand(game.getPlayer(), game);
            ICommand Bomb = new PlayerBombCommand(game.getPlayer(), game);
            ICommand Boomerang = new PlayerBoomerangCommand(game.getPlayer(), game);
            ICommand Hurt = new PlayerHurtCommand(game.getPlayer(), game);
            ICommand Left = new PlayerChangeDirectionCommand(game.getPlayer(), game, 3);
            ICommand Up = new PlayerChangeDirectionCommand(game.getPlayer(), game, 0);
            ICommand Down = new PlayerChangeDirectionCommand(game.getPlayer(), game, 1);
            ICommand Right = new PlayerChangeDirectionCommand(game.getPlayer(), game, 2);
            //KeyMap.Add(Keys.A, new PlayerAttackCommand(game.player));
            KeyMap = new Dictionary<Keys, ICommand>();
            KeyMap.Add(Keys.Z, Attack);
            KeyMap.Add(Keys.R, Attack);
            //KeyMap.Add(Keys.A, Move);
            KeyMap.Add(Keys.D1, Arrow);
            KeyMap.Add(Keys.D2, Bomb);
            KeyMap.Add(Keys.D3, Boomerang);
            KeyMap.Add(Keys.E, Hurt);
            KeyMap.Add(Keys.W, Up);
            KeyMap.Add(Keys.A, Left);
            KeyMap.Add(Keys.S, Down);
            KeyMap.Add(Keys.D, Right);
            //KeyMap.Add(Keys.Y, Idle);
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
            if ((!prevState.IsKeyDown(Keys.A) && !state.IsKeyUp(Keys.A)) ||
                (!prevState.IsKeyDown(Keys.W) && !state.IsKeyUp(Keys.W)) ||
                (!prevState.IsKeyDown(Keys.D) && !state.IsKeyUp(Keys.D)) ||
                (!prevState.IsKeyDown(Keys.S) && !state.IsKeyUp(Keys.S))) Move.Execute();
            if ((prevState.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.A)) ||
                (prevState.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.W)) ||
                (prevState.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.D)) ||
                (prevState.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.S))) Idle.Execute();

            prevState = state;
        }

    }
}
