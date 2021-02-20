using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ExplosionPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;

        int x;
        int y;
        int xVel;
        int yVel;

        

        int frameCount;
        int Lifetime = 12;

        int frameWait;
        int frameDelay;
        Color color = Color.White;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;

        public ExplosionPlayerProjectile(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
   

            frameDelay = Lifetime / 4;


            //directions, up, down, right, left

            //frames
            frames.AddLast(new Rectangle(55, 87, 16, 16));
            frames.AddLast(new Rectangle(55, 104, 16, 16));
            frames.AddLast(new Rectangle(55, 121, 16, 16));

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

            destRect = new Rectangle(x, y, currentFrame.Value.Width * 2, currentFrame.Value.Height * 2);
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
            x += xVel;
            y += yVel;
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
