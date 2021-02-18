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
        int x;
        int y;

        const int width = 11;
        const int height = 15;
        Rectangle sourceRect = new Rectangle(0, 0, width, height);
        Color color = Color.White;



        public ClockItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
            destRect = new Rectangle(width, height, x, y);
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
