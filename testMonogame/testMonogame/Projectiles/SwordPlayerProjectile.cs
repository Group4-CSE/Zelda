using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class SwordPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;

        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;

        int frameWait;
        int frameDelay = 4;

        Color color = Color.White;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;

        public SwordPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int direction)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;



            //directions, up, down, right, left
            //if 
            int widthMod = (direction/2)+1;
            direction = direction++;
            //frames
            frames.AddLast(new Rectangle(0 , 35 + (17 * (direction )), 8 * widthMod, 16));
            frames.AddLast(new Rectangle((8*widthMod)+1, 35 + (17 * (direction)), 8 * widthMod, 16));
            frames.AddLast(new Rectangle(2*((8 * widthMod) + 1), 35 + (17 * (direction )), 8 * widthMod, 16));

            frameWait = 0;

            currentFrame = frames.First;



        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void delete(Game1 game)
        {
            game.AddPlayerProjectile(new SwordboomPlayerProjectile(texture, new Vector2(X, Y), new Vector2(-1, -1), 0));
            game.AddPlayerProjectile(new SwordboomPlayerProjectile(texture, new Vector2(X, Y), new Vector2(1, 1), 1));
            game.AddPlayerProjectile(new SwordboomPlayerProjectile(texture, new Vector2(X, Y), new Vector2(1, -1), 2));
            game.AddPlayerProjectile(new SwordboomPlayerProjectile(texture, new Vector2(X, Y), new Vector2(-1, 1), 3));
            game.RemovePlayerProjectile(this);
        }

        public void doDamage(IEnemy target)
        {
            target.takeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(X, Y, currentFrame.Value.Width * 2, currentFrame.Value.Height * 2);
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


        public void Update(Game1 game)
        {
            Move();
            //TEMP collision stuff
            if (X < 0 || X > 800 || Y < 0 || Y > 480)
            {
                delete(game);
            }
        }
    }
}
