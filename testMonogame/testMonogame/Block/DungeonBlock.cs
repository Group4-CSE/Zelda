using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;

namespace testMonogame
{
    class DungeonBlock : IObject, ISprite, IBlock
    {

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(17, 0, 16, 16);
        Color color = Color.White;

        public DungeonBlock(Texture2D incTexture, Vector2 pos)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);
            orig = new Rectangle(X, Y, 32, 32);
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            //no need to update block
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
