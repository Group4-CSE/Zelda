using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using testMonogame.Interfaces;

namespace testMonogame
{
    public class PlayerProjectileEnemyCollision
    {
        Rectangle projectileRect;
        Rectangle enemyRect;
        Rectangle collision;
        int xCollisionSize;
        int yCollisionSize;

        public PlayerProjectileEnemyCollision()
        {
        }


        public void detectCollision(List<IPlayerProjectile> projectiles, List<IEnemy> enemies, GameManager game, IRoom room)
        {
            IPlayerProjectile[] projArry = projectiles.ToArray();
            foreach (var projectile in projArry)
            {
                projectileRect = projectile.getDestRect();
                IEnemy[] enemyArr = enemies.ToArray();
                foreach (var enemy in enemyArr)
                {
                    enemyRect = enemy.getDestRect();
                    collision = Rectangle.Intersect(projectileRect, enemyRect);
                    if (!collision.IsEmpty && !(projectile is SwordboomPlayerProjectile)) handleCollision(projectile,enemy, game,room );
                }
            }
        }

        public void handleCollision(IPlayerProjectile projectile, IEnemy enemy, GameManager game, IRoom room)
        {
            projectile.doDamage(enemy);
            projectile.collide(game);
            if (enemy.getHealth() <= 0) room.RemoveEnemy(enemy);
        }
    }
}
