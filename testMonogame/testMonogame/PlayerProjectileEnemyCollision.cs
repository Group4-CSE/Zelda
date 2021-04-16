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


        public void detectCollision(List<IPlayerProjectile> projectiles, List<IEnemy> enemies, GameManager game, IRoom room, Sounds sounds)
        {
            IPlayerProjectile[] projArry = projectiles.ToArray();

            foreach (var projectile in projArry)
            {
                projectileRect = projectile.getDestRect();
                IEnemy[] enemyArr = enemies.ToArray();
                bool exception = projectile is SwordboomPlayerProjectile || projectile is BombPlayerProjectile || projectile is ExplosionPlayerProjectile;
                foreach (var enemy in enemyArr)
                {
                    enemyRect = enemy.getDestRect();
                    collision = Rectangle.Intersect(projectileRect, enemyRect);
                    if (!collision.IsEmpty && !exception) handleCollision(projectile,enemy, game,room, sounds );
                }
            }
        }

        public void handleCollision(IPlayerProjectile projectile, IEnemy enemy, GameManager game, IRoom room, Sounds sounds)
        {
            projectile.doDamage(enemy, sounds);
            projectile.collide(game);
            if (enemy.getHealth() <= 0)
            {
                sounds.EnemyHitDie(1);
                room.RemoveEnemy(enemy);
            }
                
        }
    }
}
