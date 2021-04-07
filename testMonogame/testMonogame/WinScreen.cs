using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class WinScreen
    {
        Texture2D texture;


        Rectangle blackBackground = new Rectangle(0, 0, 256, 175);
        Rectangle destRect = new Rectangle(130, 0, 256 * 2, 175 * 2);
        public WinScreen(Texture2D intexture)
        {
            texture = intexture;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, blackBackground, Color.Black);



        }
        public void Update(GameManager game)
        {
            //NA
        }
    }
}
