using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class StairsBlock : IObject, ISprite
    {

        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(51, 17, 16, 16);
        Color color = Color.White;

        public StairsBlock(Texture2D incTexture, Vector2 pos)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);
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
            //Send player to room 3
        }
    }
}
