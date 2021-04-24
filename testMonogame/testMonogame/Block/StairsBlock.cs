using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;

namespace testMonogame
{
    class StairsBlock : IObject, ISprite, IDoor, IBlock
    {

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(51, 17, 16, 16);
        Color color = Color.White;

        Boolean isClosed;

        public StairsBlock(Texture2D incTexture, Vector2 pos)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);
            orig = new Rectangle(X, Y, 32, 32);
            isClosed = false;
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            //open door, no need to update anything
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            //open door, no need for interaction
        }

        public void openDoor()
        {
            isClosed = false;
        }

        public Boolean getIsClosed()
        {
            return isClosed;
        }

        public int getSide()
        {
            //side is 4 for stairs chase
            return 4;
        }

        public int getNextRoom()
        {
            //stairs lead to room 3
            return 3;
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
