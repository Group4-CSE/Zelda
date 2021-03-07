using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class DragonBlock : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect = new Rectangle(51, 0, 16, 16);
        Rectangle destRect;
        Color color = Color.White;

        //location stuff
        public int X { get; set; }
        public int Y { get; set; }
        const int width = 32;
        const int height = 32;


        public DragonBlock(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(X, Y, width, height);
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            //Nothing Needed here
        }

        public void Update(Game1 game)
        {
            //Nothing Needed here
        }
    }
}

