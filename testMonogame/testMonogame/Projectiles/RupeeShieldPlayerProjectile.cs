using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class RupeeShieldPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }

        int width = 16;
        int height = 32;
        Color color = Color.White;


        int projectileHealth = 6; //can stop 6 enemy collisions or projectiles

        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameWait;
        int frameDelay = 2;
        public RupeeShieldPlayerProjectile(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;


            frames.AddLast(new Rectangle(0, 119, 8, 16));
            frames.AddLast(new Rectangle(9, 119, 8, 16));
            currentFrame = frames.First;


            frameWait = 0;


        }
        public void collide(GameManager game)
        {
            projectileHealth--;
            if (projectileHealth <= 0) delete(game);
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
            //NA
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
            //if we collide with an enemy push them out using our collision rules instead of damaging them
            Rectangle enemyRect = target.getDestRect();
            Rectangle collisionRect = Rectangle.Intersect(destRect, enemyRect);
            int xCollisionSize = collisionRect.Width;
            int yCollisionSize = collisionRect.Height;
            // We are in a Top / Bottom style collision
            if (xCollisionSize > yCollisionSize)
            {
                // IF the bottom left corner of our enemy is less than the the top left corner of the block
                // AND the bottom left corner of our enemy is greater than the half way point of the height of the block
                // We are on the top
                if (enemyRect.Y < destRect.Y)
                {
                    target.Y -= yCollisionSize;
                }
                else // We are on the bottom
                {
                    target.Y += yCollisionSize;
                }
            }
            else // We are in a Left / Right style collision
            {
                // IF the top left corner of our enemy is less than the top right corner of the block
                // AND the top left corner of our enemy is greater than the half way point of the width of the block
                // We are on the left
                if (enemyRect.X < destRect.X)
                {
                    target.X -= xCollisionSize;
                }
                else // We are on the right
                {
                    target.X += xCollisionSize;
                }
            }
        }

    }
}
