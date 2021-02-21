using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class FireBallEnemyProjectile : IEnemyProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        int x;
        int y;
        int xVel;
        int yVel;
        int width = 8;
        int height = 16;
        Color color = Color.White;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameWait;
        int frameDelay = 2;
        public FireBallEnemyProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;

            frames.AddLast(new Rectangle(75, 33, 8, 16));
            frames.AddLast(new Rectangle(84, 33, 8, 16));
            frames.AddLast(new Rectangle(93, 33, 8, 16));
            frames.AddLast(new Rectangle(102, 33, 8, 16));
            currentFrame = frames.First;


            frameWait = 0;


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
            destRect = new Rectangle(x, y, width, height);
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
            x += xVel;
            y += yVel;
        }


        public void Update(Game1 game)
        {
            Move();
            //TEMP collision stuff
            if(x<0 || x > 800 || y < 0 || y > 480)
            {
                delete(game);
            }
        }
    }
}
