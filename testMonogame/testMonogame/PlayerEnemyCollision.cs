using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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

        public void playerEnemyDetection(IPlayer player, List<IEnemy> enemies)
        {
            //Detect player enemy collision

            foreach (IEnemy enemy in enemies)
            {
                enemyRect = enemy.getDestRect();
                //playerRect = player.getDestRect();

                playerRect = new Rectangle((int)player.X, (int)player.Y, width, height);

                collision = enemyRect.Intersects(playerRect);

                //If detected, have player take damage
                if (collision)
                {
                    playerEnemyHandler(player, enemy);
                }
            }

        }

        public void playerEnemyHandler(IPlayer player, IEnemy enemy)
        {
            player.TakeDamage(1);

        }
    }
}

