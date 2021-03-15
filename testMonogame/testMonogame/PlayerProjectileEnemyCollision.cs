using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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


        public void detectCollision(List<IPlayerProjectile> projectiles, List<IEnemy> enemies, GameManager game)
        {
            IPlayerProjectile[] projArry = projectiles.ToArray();
            foreach (var projectile in projArry)
            {
                projectileRect = projectile.getDestRect();
                foreach (var enemy in enemies)
                {
                    enemyRect = enemy.getDestRect();
                    collision = Rectangle.Intersect(projectileRect, enemyRect);
                    if (!collision.IsEmpty) handleCollision(projectile,enemy, game);
                }
            }
        }

        public void handleCollision(IPlayerProjectile projectile, IEnemy enemy, GameManager game)
        {
            projectile.doDamage(enemy);
            projectile.collide(game);
        }
    }
}
