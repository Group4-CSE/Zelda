using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class EnemyWallCollision
    {
        Rectangle enemyRect;
        Rectangle wallRect;


        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IEnemy> Enemies, Rectangle walls, Rectangle floor)
        {

            
            wallRect = walls;
            foreach (IEnemy enemy in Enemies)
            {
                enemyRect = enemy.getDestRect();

                Rectangle wallCollision = Rectangle.Intersect(enemyRect, walls);
                Rectangle floorCollision = Rectangle.Intersect(enemyRect, floor);
                //for true collision x and y are arbitrary, width and height matter
                Rectangle trueCollision = new Rectangle(wallCollision.X, wallCollision.Y, floorCollision.Width - wallCollision.Width, floorCollision.Height - wallCollision.Height);
                if (!wallCollision.Equals(floorCollision)) handleCollision(trueCollision, enemy);
            }
        }

        public void handleCollision(Rectangle collisionRect, IEnemy enemy)
        {
            xCollisionSize = collisionRect.Width;
            yCollisionSize = collisionRect.Height;
            // We are in a Top / Bottom style collision
            if (xCollisionSize > yCollisionSize)
            {
                // IF the bottom left corner of our enemy is less than the the top left corner of the block
                // AND the bottom left corner of our enemy is greater than the half way point of the height of the block
                // We are on the top
                if (enemyRect.Y < wallRect.Y + (wallRect.Height / 2))
                {
                    enemy.Y -= yCollisionSize;
                }
                else // We are on the bottom
                {
                    enemy.Y += yCollisionSize;
                }
            }
            else // We are in a Left / Right style collision
            {
                // IF the top left corner of our enemy is less than the top right corner of the block
                // AND the top left corner of our enemy is greater than the half way point of the width of the block
                // We are on the left
                if (enemyRect.X < wallRect.X + (wallRect.Width / 2))
                {
                    enemy.X -= xCollisionSize;
                }
                else // We are on the right
                {
                    enemy.X += xCollisionSize;
                }
            }
        }
    }
}
