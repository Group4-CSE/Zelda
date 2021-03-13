using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class EnemyProjectileWallCollision
    {
        Rectangle projRect;
        Rectangle wallRect;


        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IEnemyProjectile> projectiles, Rectangle walls, Rectangle floor, Game1 game)
        {


            wallRect = walls;
            IEnemyProjectile[] projectilesArr = projectiles.ToArray();
            foreach (IEnemyProjectile projectile in projectilesArr)
            {
                projRect = projectile.getDestRect();

                Rectangle wallCollision = Rectangle.Intersect(projRect, walls);
                Rectangle floorCollision = Rectangle.Intersect(projRect, floor);
                //for true collision x and y are arbitrary, width and height matter
                Rectangle trueCollision = new Rectangle(wallCollision.X, wallCollision.Y, floorCollision.Width - wallCollision.Width, floorCollision.Height - wallCollision.Height);
                if (!wallCollision.Equals(floorCollision)) handleCollision(projectile, game);
            }
        }

        public void handleCollision(IEnemyProjectile projectile, Game1 game)
        {
            projectile.collide(game);
        }
    }
}
