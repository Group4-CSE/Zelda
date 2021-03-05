using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class BoomerangPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;
        int width = 16;
        int height = 32;
        Color color = Color.White;
        int initialX;
        int initialY;
        int initialXVel;
        int initialYVel;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameWait;
        int frameDelay = 2;
        public BoomerangPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int frameOffSet)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;

            initialX = X;
            initialY = Y;
            initialXVel=xVel;
            initialYVel=yVel;
            frames.AddLast(new Rectangle(0, 0, 8, 16));
            frames.AddLast(new Rectangle(9, 0, 8, 16));
            frames.AddLast(new Rectangle(18, 0, 8, 16));
            frames.AddLast(new Rectangle(27, 0, 8, 16));
            frames.AddLast(new Rectangle(36, 0, 8, 16));
            frames.AddLast(new Rectangle(45, 0, 8, 16));
            frames.AddLast(new Rectangle(54, 0, 8, 16));
            frames.AddLast(new Rectangle(63, 0, 8, 16));

            currentFrame = frames.First;

            for(int i=0; i < frameOffSet; i++)
            {
                currentFrame = currentFrame.Next;
            }


            frameWait = 0;


        }

        public void delete(Game1 game)
        {
            game.RemovePlayerProjectile(this);
        }

        public void doDamage(IEnemy target)
        {
            target.takeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, width, height);
            frameWait++;
            if (frameWait > frameDelay)
            {
                if (currentFrame.Next != null) currentFrame = currentFrame.Next;
                else currentFrame = frames.First;
                frameWait = 0;
            }

            spriteBatch.Draw(texture, destRect, currentFrame.Value, color);




        }
    
        public void Move()
        {
            X += xVel;
            Y += yVel;
        }

        bool testStop()
        {
            bool res=false;
            if (initialXVel < 0 && initialYVel == 0) res = (X > initialX);
            else if (initialXVel > 0 && initialYVel == 0) res = (X < initialX);
            else if (initialYVel < 0 && initialXVel == 0) res = (Y > initialY);
            else if (initialYVel > 0 && initialXVel == 0) res = (Y < initialY);
            return res;

        }
        public void Update(Game1 game)
        {
            Move();
            //TEMP collision stuff
            if (X < 0 || X > 800 || Y < 0 || Y > 480)
            {
                xVel *= -1;
                yVel *= -1;
            }
            if (testStop())
            {
                game.getPlayer().ObtainItem("Boomerang");
                delete(game);
            }
        }
    }
}
