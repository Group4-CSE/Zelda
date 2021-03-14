using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;

namespace testMonogame
{
    public class PlayerEnemyCollision
    {
        public PlayerEnemyCollision()
        {
        }

        Rectangle playerRect;
        Rectangle enemyRect;
        Boolean collision;

        const int height = 16;
        const int width = 16;

        public void playerEnemyDetection(IPlayer player, List<IEnemy> enemies,IRoom room)
        {
            //Detect player enemy collision
            IEnemy[] enemyArr = enemies.ToArray();
            foreach (IEnemy enemy in enemyArr)
            {
                enemyRect = enemy.getDestRect();
                //playerRect = player.getDestRect();

                playerRect = new Rectangle((int)player.X, (int)player.Y, width, height);

                collision = enemyRect.Intersects(playerRect);

                //If detected, have player take damage
                if (collision)
                {
                    playerEnemyHandler(Rectangle.Intersect(playerRect,enemyRect),player, enemy,room);
                }
            }

        }

        public void playerEnemyHandler(Rectangle collisionRect,IPlayer player, IEnemy enemy,IRoom room)
        {
            //get direction
            int direction;
            //0=up, 1=right, 2=down, 3= left
            int xCollisionSize = collisionRect.Width;
            int yCollisionSize = collisionRect.Height;
            // We are in a Top / Bottom style collision
            if (xCollisionSize > yCollisionSize)
            {
                // IF the bottom left corner of our enemy is less than the the top left corner of the block
                // AND the bottom left corner of our enemy is greater than the half way point of the height of the block
                // We are on the bottom
                if (collisionRect.Y == enemyRect.Y)
                {
                    direction = 2;
                }
                else // We are on the top
                {
                    direction = 0;
                }
            }
            else // We are in a Left / Right style collision
            {
                // IF the top left corner of our enemy is less than the top right corner of the block
                // AND the top left corner of our enemy is greater than the half way point of the width of the block
                // We are on the right
                if (collisionRect.X == enemyRect.X)
                {
                    direction = 1;
                }
                else // We are on the left
                {
                    direction = 3;
                }
            }

            if (player.IsAttacking() || direction==player.GetDirection())
            {
                player.dealDamage(enemy);
                if (enemy.getHealth() <= 0) room.RemoveEnemy(enemy);
                
                
            }
            else
            {
                enemy.Attack(player);
                //game over if health<=0. do this later once we have game over procedure.
            }

        }
    }
}

