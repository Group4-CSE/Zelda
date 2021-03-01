using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class HeartItem : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect;
        Rectangle destRect;
        Color color = Color.White;
        int currentFrame;
        int frameDelay;

        //location stuff
        int x;
        int y;
        const int width = 14;
        const int height = 16;


        public HeartItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;

            //Non-moving item so instantiate dest rectangle
            destRect = new Rectangle(x, y, width, height);

            currentFrame = 0;
            frameDelay = 0;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            frameDelay++;
            if (frameDelay >= 15)
            {
                currentFrame++;
                if (currentFrame > 1) currentFrame = 0;
                frameDelay = 0;
            }

            //MAYBE ADD DESPAWNING RULES HERE. IDK IF IT DESPAWNS OR NOT 


            sourceRect =  new Rectangle(37+(currentFrame*8), 0, 7, 8);


            spriteBatch.Draw(texture, destRect, sourceRect, color);

        }

        public void Interact(IPlayer player)
        {
            //TODO: Add code for picking up heart (heal player)
        }

        public void Update(Game1 game)
        {
            //Nothing Needed here
        }
    }
}
