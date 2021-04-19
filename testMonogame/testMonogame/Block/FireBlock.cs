using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using testMonogame.Interfaces;

namespace testMonogame
{
    class FireBlock : IObject, ISprite, IBlock
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect;
        Rectangle destRect;
        Rectangle orig;
        Color color = Color.White;
        int currentframe;
        int framedelay;

        //location stuff
        public int X { get; set; }
        public int Y { get; set; }
        const int width = 32;
        const int height = 32;


        public FireBlock(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(X, Y, width, height);
            orig = new Rectangle(X, Y, width, height);

        }
        public Rectangle getDestRect()
        {
            return destRect;
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
          
            spriteBatch.Draw(texture, destRect, sourceRect, color);
            
        }

        public void Interact(IPlayer player)
        {
            //Nothing Needed here
        }

        public void Update(GameManager game)
        {
            //Nothing Needed here
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
