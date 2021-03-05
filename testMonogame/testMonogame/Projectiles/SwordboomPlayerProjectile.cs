using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class SwordboomPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;

        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;

        int frameWait;
        int frameDelay = 4;

        int frameCount;
        int Lifetime = 12;

        Color color = Color.White;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;

        public SwordboomPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int direction)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;



            //directions, up, down, right, left
            //if 
            
            direction = direction++;
            //frames
            frames.AddLast(new Rectangle(55, 19 + (17 * (direction)), 8 , 16));
            frames.AddLast(new Rectangle(55 +9, 19 + (17 * (direction)), 8 , 16));
            frames.AddLast(new Rectangle(55 +18, 19 + (17 * (direction)), 8, 16));

            frameWait = 0;
            frameCount = 0;

            currentFrame = frames.First;



        }

        public void delete(Game1 game)
        {
            game.RemovePlayerProjectile(this);
        }

        public void doDamage(IEnemy target)
        {
            //Visual projectile only
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(X, Y, currentFrame.Value.Width * 2, currentFrame.Value.Height * 2);
            frameWait++;
            if (frameWait > frameDelay)
            {
                frameCount++;
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


        public void Update(Game1 game)
        {
            Move();
            //dissapear
            if (frameCount > Lifetime)
            {
                delete(game);
            }
        }
    }
}
