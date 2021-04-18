using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;

namespace testMonogame
{
    public class SolidBlockDoor : IObject, ISprite, IDoor
    {

        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(30, 200, 16, 16);
        Color color;

        public Boolean isClosed;

        public SolidBlockDoor(Texture2D incTexture, Vector2 pos, Color inColor)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);

            color = inColor;
            isClosed = false;
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            // COLLISION WILL GO HERE
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            // NULL
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
            //currently only used for special room 3
            return 5;
        }

        public int getNextRoom()
        {
            //used to return to room 1
            return 1;
        }
    }
}
