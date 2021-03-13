using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class BoomerangEnemyProjectile : IEnemyProjectile, ISprite

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
        GoriyaEnemy goriya;
        bool turned;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameWait;
        int frameDelay = 2;
        public BoomerangEnemyProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int frameOffSet, GoriyaEnemy inGoriya)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;
            turned = false;
            initialX = X;
            initialY = Y;
            initialXVel = xVel;
            initialYVel = yVel;
            frames.AddLast(new Rectangle(0, 0, 8, 16));
            frames.AddLast(new Rectangle(9, 0, 8, 16));
            frames.AddLast(new Rectangle(18, 0, 8, 16));
            frames.AddLast(new Rectangle(27, 0, 8, 16));
            frames.AddLast(new Rectangle(36, 0, 8, 16));
            frames.AddLast(new Rectangle(45, 0, 8, 16));
            frames.AddLast(new Rectangle(54, 0, 8, 16));
            frames.AddLast(new Rectangle(63, 0, 8, 16));

            currentFrame = frames.First;

            goriya = inGoriya;

            for (int i = 0; i < frameOffSet; i++)
            {
                currentFrame = currentFrame.Next;
            }


            frameWait = 0;


        }
        public void collide(Game1 game)
        {
            if (turned == false)
            {
                xVel *= -1;
                yVel *= -1;
                turned = true;
            }
            
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void delete(Game1 game)
        {
            game.RemoveEnemyProjectile(this);
        }

        public void doDamage(IPlayer player)
        {
            player.TakeDamage(1);
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
            bool res = false;
            if (initialXVel < 0 && initialYVel == 0) res = (X > initialX);
            else if (initialXVel > 0 && initialYVel == 0) res = (X < initialX);
            else if (initialYVel < 0 && initialXVel == 0) res = (Y > initialY);
            else if (initialYVel > 0 && initialXVel == 0) res = (Y < initialY);
            return res;

        }
        public void Update(Game1 game)
        {
            Move();
            if (testStop())
            {
                goriya.setThrow(false);
                delete(game);
            }
        }
    }
}
