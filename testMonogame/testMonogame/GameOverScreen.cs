using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class GameOverScreen
    {
        Texture2D texture;
        SpriteFont header;
        SpriteFont font;

        Rectangle blackBackground = new Rectangle(0, 0, 256, 175);
        Rectangle destRect = new Rectangle(130, 0, 256 * 2, 175 * 2);
        public GameOverScreen(Texture2D intexture, SpriteFont smallFont, SpriteFont bigFont)
        {
            texture = intexture;
            font = smallFont;
            header = bigFont;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, blackBackground, Color.Black);
            spriteBatch.DrawString(header, "Game Over", new Vector2(200, 0), Color.White);
            spriteBatch.DrawString(font, "Press any button to continue", new Vector2(300, 100), Color.White);


        }
        public void Update(GameManager game)
        {
            //NA
        }
    }
}
