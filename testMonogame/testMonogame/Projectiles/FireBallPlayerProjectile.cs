using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class FireBallPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;
        int width = 8;
        int height = 16;
        Color color = Color.White;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameWait;
        int frameDelay = 2;
        public FireBallPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;

            frames.AddLast(new Rectangle(0, 105, 8, 10));
            frames.AddLast(new Rectangle(9, 105, 8, 10));
            frames.AddLast(new Rectangle(18, 105, 8, 10));
            frames.AddLast(new Rectangle(27, 105, 8, 10));
            currentFrame = frames.First;


            frameWait = 0;


        }
        public void collide(GameManager game)
        {
            delete(game);
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void delete(GameManager game)
        {
            game.RemovePlayerProjectile(this);
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


        public void Update(GameManager game)
        {
            Move();
            //TEMP collision stuff
            if (X < 0 || X > 800 || Y < 0 || Y > 480)
            {
                delete(game);
            }
        }

        public void doDamage(IEnemy target, Sounds sounds)
        {
            target.takeDamage(1);
        }
    }
}
