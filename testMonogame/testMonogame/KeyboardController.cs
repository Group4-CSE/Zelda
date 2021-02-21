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
        List<Keys> moveKeys;

        Dictionary<Keys, int> direcPriority;
        int moveTotal;
        Keys direcPressed;

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

            direcPriority = new Dictionary<Keys, int>();
            direcPriority.Add(Keys.W, 0);
            direcPriority.Add(Keys.A, 0);
            direcPriority.Add(Keys.S, 0);
            direcPriority.Add(Keys.D, 0);

            moveKeys = new List<Keys>();
            moveKeys.Add(Keys.W);
            moveKeys.Add(Keys.A);
            moveKeys.Add(Keys.S);
            moveKeys.Add(Keys.D);

            direcPressed = Keys.I;
            moveTotal = 0;
            
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            direcPressed = Keys.I;

            //run the command associated with any key pressed.
            foreach (Keys k in state.GetPressedKeys())
            {
                //only attempt to execute if the key is present in the dictionary
                if (KeyMap.ContainsKey(k) && !prevState.IsKeyDown(k))
                {
                    KeyMap[k].Execute();
                    if(k.Equals(Keys.W)|| k.Equals(Keys.A) || k.Equals(Keys.S) || k.Equals(Keys.D))
                    {
                        direcPressed = k;
                    }
                }
                
            }
            //            if ((!prevState.IsKeyDown(Keys.A) && !state.IsKeyUp(Keys.A)) ||
            //                (!prevState.IsKeyDown(Keys.W) && !state.IsKeyUp(Keys.W)) ||
            //                (!prevState.IsKeyDown(Keys.D) && !state.IsKeyUp(Keys.D)) ||
            //                (!prevState.IsKeyDown(Keys.S) && !state.IsKeyUp(Keys.S))) Move.Execute();
            //            if ((prevState.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.A)) ||
            //                (prevState.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.W)) ||
            //                (prevState.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.D)) ||
            //                (prevState.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.S))) Idle.Execute();
            handleMovement(state);


            prevState = state;
        }

        public void handleMovement(KeyboardState state)
        {
            if (!direcPressed.Equals(Keys.I))
            {
                direcPriority[direcPressed] = 5;
                moveTotal += 5;
                Move.Execute();
            }

            //check for released keys and adjust dictionary
            if(moveTotal != 0)
            {
                foreach (Keys direction in moveKeys)
                {
                    if(direcPriority[direction] != 0)
                    {
                        //check for key up
                        if(prevState.IsKeyDown(direction) && state.IsKeyUp(direction)){

                            if (direcPressed.Equals(Keys.I))
                            {
                                if(direcPriority[direction] == 5)
                                {
                                    adjustCurDirec(direction);
                                }
                            }
                            else
                            {
                                if (!direction.Equals(direcPressed)&& direcPriority[direction]==5)
                                {
                                    adjustCurDirec(direction);
                                }
                            }
                            
                            moveTotal = moveTotal - direcPriority[direction];
                            direcPriority[direction] = 0;
                            
                        }
                        else
                        {
                            //key is still pressed down
                            if (!direcPressed.Equals(Keys.I))
                            {
                                //check if key was previously pressed
                                if (!direcPressed.Equals(direction))
                                {
                                    if (direcPriority[direction] != 0)
                                    {
                                        direcPriority[direction] = direcPriority[direction] - 1;
                                        moveTotal--;
                                    }
                                } 
                                
                            }
                        }
                    }
                }
            }
            if (moveTotal == 0)
            {
                Idle.Execute();
            }
        }

        public void adjustCurDirec(Keys released)
        {
            moveTotal = moveTotal - direcPriority[released];
            direcPriority[released] = 0;
            int highest = 0;
            Keys highestKey = Keys.I;

            if(moveTotal != 0)
            {
                foreach (Keys direction in moveKeys)
                {
                    if (direcPriority[direction] > highest)
                    {
                        highest = direcPriority[direction];
                        highestKey = direction;

                    }
                }
                if (!highestKey.Equals(Keys.I))
                {   //change direction to current direction
                    moveTotal = moveTotal - highest + 5;
                    direcPriority[highestKey] = 5;
                    KeyMap[highestKey].Execute();
                    Move.Execute();
                }
                

            }
            
        }


    }
}
