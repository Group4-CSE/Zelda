﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;
namespace testMonogame
{
    public class CaveDoor : ISprite, IObject, IDoor, IBlock
    {
        public int X { get; set; }
        public int Y { get; set; }

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        Rectangle sourceRect;
        Boolean isClosed;
        int side;

        int nextRoom;

        public CaveDoor(int direction, Vector2 pos, Texture2D texture, int next,Boolean closed)
        {
            this.texture = texture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 65, 65);
            orig = new Rectangle(X, Y, 65, 65);
            side = direction;
            isClosed = closed;
            nextRoom = next;

            switch (direction)
            {
                case 0:     //north door
                    sourceRect = new Rectangle(99, 0, 32, 32);
                    break;
                case 1:     //west door
                    sourceRect = new Rectangle(99, 33, 32, 32);
                    break;
                case 2:     //east door
                    sourceRect = new Rectangle(99, 66, 32, 32);
                    break;
                case 3:     //south door
                    sourceRect = new Rectangle(99, 99, 32, 32);
                    break;
                default:
                    sourceRect = new Rectangle(99, 0, 32, 32);
                    break;

            }
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            //open door, no need for update
        }

        public void Interact(IPlayer player)
        {
            //open door, no need for interaction
        }

        public void openDoor()
        {
            isClosed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if the cave door is closed/hidden don't draw
            if (!isClosed)
            {
                spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
            }
            
        }

        public Boolean getIsClosed()
        {
            return isClosed;
        }

        public int getSide()
        {
            return side;
        }

        public int getNextRoom()
        {
            return nextRoom;
        }

        public void transitionShift(int x, int y)
        {
            destRect.X = destRect.X + x;
            destRect.Y = destRect.Y + y;
        }
        public void resetToOriginalPos()
        {
            destRect.X = orig.X;
            destRect.Y = orig.Y;
        }
    }
}
