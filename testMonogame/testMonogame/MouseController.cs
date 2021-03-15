using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using testMonogame.Commands;

namespace testMonogame
{
    class MouseController : IController
    {
        const int mapX = 0;
        const int mapY = 0;
        const int finalMapWidth = 141;
        const int finalMapHeight = 69;

        GameManager game;
        public MouseController(GameManager inGame)
        {
            game = inGame;
        }
        public void Update()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && state.X<=finalMapWidth && state.X>=mapX
                && state.Y<=finalMapHeight && state.Y>=mapY)
            {
                //only if left click is down and we are clicking on the map will we even try to load
                int x = (state.X / ((finalMapWidth-mapX) / 6)) + 1;
                int y = (state.Y / ((finalMapHeight-mapY) / 6)) + 1;
                ICommand cmd = new LoadRoomFromMapClickCommand(game, x, y);
                cmd.Execute();
            }
        }
    }
}
