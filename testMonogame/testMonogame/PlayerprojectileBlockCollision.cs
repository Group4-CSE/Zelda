using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class PlayerprojectileBlockCollision
    {
        Rectangle projectileRect;
        Rectangle blockRect;
        Rectangle collision;
        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IPlayerProjectile> projectiles, List<IObject> blocks, Game1 game)
        {
            foreach (var projectile in projectiles)
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

        public void handleCollision(IPlayerProjectile projectile, Game1 game)
        {
            projectile.delete(game);
        }
    }
}
