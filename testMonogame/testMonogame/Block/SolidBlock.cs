using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class SolidBlock : IObject, ISprite, IBlock
    {

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(30, 200, 16, 16);
        Color color;

        public SolidBlock(Texture2D incTexture, Vector2 pos, Color inColor)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);
            orig = new Rectangle(X, Y, 32, 32);

            color = inColor;
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
