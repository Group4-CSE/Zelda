using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class FairyItem : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect;
        Rectangle destRect;
        Color color = Color.White;
        int currentFrame;
        int frameDelay;

        //location stuff
        public int X { get; set; }
        public int Y { get; set; }
        const int width = 16;
        const int height = 34;
        //movement
        int sideMoveTimer;
        int xVelocity;
        int yVelocity;

        public FairyItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            
            

            currentFrame = 0;
            frameDelay = 0;

            sideMoveTimer = 0;
            //start it moving up. if it hits a wall it will reverse
            yVelocity = -1;
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


            sourceRect = new Rectangle(27 + (currentFrame * 9), 13, 8, 17);
            destRect = new Rectangle(X, Y, width, height);

            spriteBatch.Draw(texture, destRect, sourceRect, color);

        }

        public void Interact(IPlayer player)
        {
            //TODO: Add code for picking up fairy (full heal link)
        }




        public void Update(Game1 game)
        {
            

            //Code for movement

            //sideways movement
            sideMoveTimer++;
            if (sideMoveTimer == 80) xVelocity = 1;
            else if (sideMoveTimer == 100) xVelocity = 0;
            else if (sideMoveTimer == 160) xVelocity = -1;
            else if (sideMoveTimer == 180)
            {
                xVelocity = 0;
                sideMoveTimer = 0;
            }


            //TODO: add collision later
            //Temporary collision for this window
            if (Y < 0 || Y+height > 480) yVelocity *= -1;


            X += xVelocity;
            Y += yVelocity;


           
        }
    }
}
