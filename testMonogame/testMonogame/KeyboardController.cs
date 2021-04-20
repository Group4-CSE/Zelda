using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using testMonogame.Commands;
namespace testMonogame
{
    public class KeyboardController : IController
    {
        Dictionary<Keys, ICommand> PlayingKeyMap;
        Dictionary<Keys, ICommand> ItemSelectionKeyMap;
        Dictionary<Keys, ICommand> PauseKeyMap;
        Dictionary<Keys, ICommand> StartKeyMap;
        KeyboardState prevState;
        List<Keys> moveKeys;

        Dictionary<Keys, int> direcPriority;
        int moveTotal;
        Keys direcPressed;

        ICommand Idle;
        ICommand Move;
        ICommand s2reset;

        GameManager manager;

        public KeyboardController(GameManager game)
        {
            prevState = Keyboard.GetState();
            manager = game;

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
            ICommand quit = new QuitCommand(game);
            ICommand nextBlock = new CycleBocksCommand(game,1);
            ICommand prevBlock = new CycleBocksCommand(game,-1);
            ICommand nextItem = new CycleItemCommand(game,1);
            ICommand prevItem = new CycleItemCommand(game,-1);
            ICommand nextEnemy = new CycleEnemyCommand(game,1);
            ICommand prevEnemy = new CycleEnemyCommand(game,-1);
            ICommand playing = new SetGameStateCommand(game, 0);
            ICommand inventorySelection = new SetGameStateCommand(game, 1);
            ICommand pause = new SetGameStateCommand(game, 2);
            ICommand lose = new SetGameStateCommand(game, 3);
            ICommand win = new SetGameStateCommand(game, 4);
            ICommand nextItemSelect = new NextItemCommand(game);
            ICommand prevItemSelect = new PreviousItemCommand(game);
            ICommand useSelectedItem = new UseSelectedItemCommand(game);
            ICommand nextStartBox = new SelectNextOptionCommand(game);
            ICommand prevStartBox = new SelectPreviousOptionCommand(game);
            ICommand prevOption = new PreviousOptionBasedOnSelectionCommand(game);
            ICommand nextOption = new NextOptionBasedOnSelectionCommand(game);


            //start key map
            StartKeyMap = new Dictionary<Keys, ICommand>();
            StartKeyMap.Add(Keys.Q, quit);
            StartKeyMap.Add(Keys.S, nextStartBox);
            StartKeyMap.Add(Keys.Down, nextStartBox);
            StartKeyMap.Add(Keys.W, prevStartBox);
            StartKeyMap.Add(Keys.Up, prevStartBox);
            StartKeyMap.Add(Keys.A, prevOption);
            StartKeyMap.Add(Keys.Left, prevOption);
            StartKeyMap.Add(Keys.D, nextOption);
            StartKeyMap.Add(Keys.Right, nextOption);



            //item selection kepMap
            ItemSelectionKeyMap = new Dictionary<Keys, ICommand>();
            ItemSelectionKeyMap.Add(Keys.C, playing);
            ItemSelectionKeyMap.Add(Keys.A, prevItemSelect);
            ItemSelectionKeyMap.Add(Keys.Left, prevItemSelect);
            ItemSelectionKeyMap.Add(Keys.D, nextItemSelect);
            ItemSelectionKeyMap.Add(Keys.Right, nextItemSelect);
            //pause  kepMap
            PauseKeyMap = new Dictionary<Keys, ICommand>();
            PauseKeyMap.Add(Keys.C, playing);


             s2reset = new S2Reset(game);
            //KeyMap.Add(Keys.A, new PlayerAttackCommand(game.player));
            PlayingKeyMap = new Dictionary<Keys, ICommand>();
            PlayingKeyMap.Add(Keys.P, pause);
            PlayingKeyMap.Add(Keys.I, inventorySelection);
            PlayingKeyMap.Add(Keys.Z, Attack);
            PlayingKeyMap.Add(Keys.N, Attack);
            PlayingKeyMap.Add(Keys.X, useSelectedItem);
            PlayingKeyMap.Add(Keys.M, useSelectedItem);
            //KeyMap.Add(Keys.A, Move);
            PlayingKeyMap.Add(Keys.D1, Arrow);
            PlayingKeyMap.Add(Keys.D2, Bomb);
            PlayingKeyMap.Add(Keys.D3, Boomerang);
            PlayingKeyMap.Add(Keys.E, Hurt);
            PlayingKeyMap.Add(Keys.W, Up);
            PlayingKeyMap.Add(Keys.A, Left);
            PlayingKeyMap.Add(Keys.S, Down);
            PlayingKeyMap.Add(Keys.D, Right);
            PlayingKeyMap.Add(Keys.Up, Up);
            PlayingKeyMap.Add(Keys.Left, Left);
            PlayingKeyMap.Add(Keys.Down, Down);
            PlayingKeyMap.Add(Keys.Right, Right);
            //KeyMap.Add(Keys.Y, Idle);

            PlayingKeyMap.Add(Keys.Escape, quit);
            //PlayingKeyMap.Add(Keys.T, prevBlock);
            //PlayingKeyMap.Add(Keys.Y, nextBlock);
            //PlayingKeyMap.Add(Keys.U, prevItem);
            //PlayingKeyMap.Add(Keys.I, nextItem);
            //PlayingKeyMap.Add(Keys.O, prevEnemy);
            //PlayingKeyMap.Add(Keys.P, nextEnemy);
            PlayingKeyMap.Add(Keys.R, s2reset);
            PlayingKeyMap.Add(Keys.Q, quit);

            direcPriority = new Dictionary<Keys, int>();
            direcPriority.Add(Keys.W, 0);
            direcPriority.Add(Keys.A, 0);
            direcPriority.Add(Keys.S, 0);
            direcPriority.Add(Keys.D, 0);
            direcPriority.Add(Keys.Up, 0);
            direcPriority.Add(Keys.Left, 0);
            direcPriority.Add(Keys.Down, 0);
            direcPriority.Add(Keys.Right, 0);


            moveKeys = new List<Keys>();
            moveKeys.Add(Keys.W);
            moveKeys.Add(Keys.A);
            moveKeys.Add(Keys.S);
            moveKeys.Add(Keys.D);
            moveKeys.Add(Keys.Up);
            moveKeys.Add(Keys.Left);
            moveKeys.Add(Keys.Down);
            moveKeys.Add(Keys.Right);

            direcPressed = Keys.I;
            moveTotal = 0;
            
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            //Debug.WriteLine(manager.getState());
            if (manager.getState()==0)
            {
                direcPressed = Keys.I;

                //run the command associated with any key pressed.
                foreach (Keys k in state.GetPressedKeys())
                {
                    //only attempt to execute if the key is present in the dictionary
                    if (PlayingKeyMap.ContainsKey(k) && !prevState.IsKeyDown(k))
                    {
                        PlayingKeyMap[k].Execute();
                        if (k.Equals(Keys.W) || k.Equals(Keys.A) || k.Equals(Keys.S) || k.Equals(Keys.D) || k.Equals(Keys.Up) || k.Equals(Keys.Left) || k.Equals(Keys.Right) || k.Equals(Keys.Down))
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



            }
            
            //Start
            else if (manager.getState() == 6)
            {
                foreach(Keys k in state.GetPressedKeys())
                {
                    if (prevState.GetPressedKeyCount() == 0 && StartKeyMap.ContainsKey(k))
                    {
                        StartKeyMap[k].Execute();
                    }
                }
            }
            //item selection
            else if (manager.getState() == 1)
            {
                foreach(Keys k in state.GetPressedKeys())
                {
                    if (prevState.GetPressedKeyCount() == 0 && ItemSelectionKeyMap.ContainsKey(k))
                    {
                        ItemSelectionKeyMap[k].Execute();
                    }
                }
            }
            //pause
            else if (manager.getState() == 2)
            {
                
                foreach (Keys k in state.GetPressedKeys())
                {
                    if (PauseKeyMap.ContainsKey(k))
                    {
                        
                        PauseKeyMap[k].Execute();
                    }
                }
            }
            //win and lose
            else if (manager.getState() == 3 || manager.getState()==4)
            {
                if(prevState.GetPressedKeyCount()==0)s2reset.Execute();
            }
            
            prevState = state;
        }

        public void handleMovement(KeyboardState state)
        {
            if (!direcPressed.Equals(Keys.I))
            {
                direcPriority[direcPressed] = 9;
                moveTotal += 9;
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
                                if(direcPriority[direction] == 9)
                                {
                                    adjustCurDirec(direction);
                                }
                            }
                            else
                            {
                                if (!direction.Equals(direcPressed)&& direcPriority[direction]==9)
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
                    moveTotal = moveTotal - highest + 9;
                    direcPriority[highestKey] = 9;
                    PlayingKeyMap[highestKey].Execute();
                    Move.Execute();

                    //up priority for keys still pressed
                    foreach (Keys direction in moveKeys)
                    {
                        if (direcPriority[direction] !=9 && direcPriority[direction] !=0)
                        {
                            direcPriority[direction] = direcPriority[direction] +1 ;
                            moveTotal++;

                        }
                    }
                }
                

            }
            
        }


    }
}
