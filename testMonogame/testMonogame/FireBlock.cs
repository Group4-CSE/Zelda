using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class FireBlock : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect;
        Rectangle destRect;
        Color color = Color.White;
        int currentframe;
        int framedelay;

        //location stuff
        int x;
        int y;
        const int width = 16;
        const int height = 16;


        public FireBlock(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(x, y, width, height);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            framedelay++;
            if (framedelay > 5)
            {
                currentframe++;
                if (currentframe > 1)
                {
                    currentframe = 0;
                }
                framedelay = 0;
            }
            sourceRect = new Rectangle(16*currentframe, 0, 16, 16);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destRect, sourceRect, color);
            spriteBatch.End();
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
