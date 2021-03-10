using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class EnemyObjectCollision
    {
        Rectangle enemyRect;
        Rectangle blockRect;
        Rectangle collision;
        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IEnemy> enemies, List<IObject> blocks)
        {
            foreach (var enemy in enemies)
            {
                enemyRect = enemy.getDestRect();
                foreach (var block in blocks)
                {
                    blockRect = block.getDestRect();
                    collision = Rectangle.Intersect(enemyRect, blockRect);
                    if (!collision.IsEmpty) handleCollision(collision, enemy, block);
                }
            }
        }

        public void handleCollision(Rectangle collisionRect, IEnemy enemy, IObject block)
        {
            xCollisionSize = collisionRect.X;
            yCollisionSize = collisionRect.Y;

            // We are in a Top / Bottom style collision
            if (xCollisionSize > yCollisionSize)
            {
                // IF the bottom left corner of our enemy is less than the the top left corner of the block
                // AND the bottom left corner of our enemy is greater than the half way point of the height of the block
                // We are on the top
                if ((enemyRect.Y + enemyRect.Height < blockRect.Y) && (enemyRect.Y + enemyRect.Height > blockRect.Y + blockRect.Height / 2))
                {
                    enemy.Y -= collisionRect.Y;
                } 
                else // We are on the bottom
                {
                    enemy.Y += collisionRect.Y;
                }
            } 
            else // We are in a Left / Right style collision
            {
                // IF the top left corner of our enemy is less than the top right corner of the block
                // AND the top left corner of our enemy is greater than the half way point of the width of the block
                // We are on the right
                if ((enemyRect.X < blockRect.X + blockRect.Width) && (enemyRect.X > blockRect.X + blockRect.Width / 2))
                {
                    enemy.X += collisionRect.X;
                }
                else // We are on the left
                {
                    enemy.X -= collisionRect.X;
                }
            }
        }
    }
}
