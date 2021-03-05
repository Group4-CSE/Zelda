using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class ClockItem : IObject, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }

        const int width = 22;
        const int height = 30;
        Rectangle sourceRect = new Rectangle(0, 0, width, height);
        Color color = Color.White;



        public ClockItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            destRect = new Rectangle(width, height, X, Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            // Implement picking up here
        }

        public void Update(Game1 game)
        {
            // NULL
        }

    }
}
