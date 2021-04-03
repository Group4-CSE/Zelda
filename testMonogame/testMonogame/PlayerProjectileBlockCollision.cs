using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class PlayerProjectileBlockCollision
    {
        Rectangle projectileRect;
        Rectangle blockRect;
        Rectangle collision;
        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IPlayerProjectile> projectiles, List<IObject> blocks, GameManager game)
        {
            IPlayerProjectile[] projArry = projectiles.ToArray();
            foreach (var projectile in projArry)
            {
                projectileRect = projectile.getDestRect();
                foreach (var block in blocks)
                {
                    blockRect = block.getDestRect();
                    collision = Rectangle.Intersect(projectileRect, blockRect);
                    if (!collision.IsEmpty) handleCollision(projectile, game);
                }
            }
        }

        public void handleCollision(IPlayerProjectile projectile, GameManager game)
        {
            projectile.collide(game);
        }
    }
}
