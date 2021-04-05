using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using testMonogame.Commands;

namespace testMonogame
{
    class MouseController : IController
    {
        //const int mapX = 0;
        //const int mapY = 0;
        const int finalMapWidth = 188;
        const int finalMapHeight = 92;

        GameManager game;
        public MouseController(GameManager inGame)
        {
            game = inGame;
        }
        public void Update()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && state.X<= game.getHUD().hudX+finalMapWidth && state.X>= game.getHUD().hudX
                && state.Y<= game.getHUD().hudY+finalMapHeight && state.Y>= game.getHUD().hudY)
            {
                //only if left click is down and we are clicking on the map will we even try to load
                int x = ((state.X-game.getHUD().hudX) / ((finalMapWidth) / 6)) + 1;
                int y = ((state.Y-game.getHUD().hudY) / ((finalMapHeight) / 6)) + 1;
                ICommand cmd = new LoadRoomFromMapClickCommand(game, x, y);
                cmd.Execute();
            }
        }
    }
}
