using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class PlayerProjectileWallCollision
    {
        Rectangle projRect;
        Rectangle wallRect;


        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(List<IPlayerProjectile> projectiles, Rectangle walls, Rectangle floor,GameManager game)
        {


            wallRect = walls;
            IPlayerProjectile[] projectilesArr = projectiles.ToArray();
            foreach (IPlayerProjectile projectile in projectilesArr)
            {
                projRect = projectile.getDestRect();

                Rectangle wallCollision = Rectangle.Intersect(projRect, walls);
                Rectangle floorCollision = Rectangle.Intersect(projRect, floor);
                //for true collision x and y are arbitrary, width and height matter
                Rectangle trueCollision = new Rectangle(wallCollision.X, wallCollision.Y, floorCollision.Width - wallCollision.Width, floorCollision.Height - wallCollision.Height);
                bool exception = projectile is SwordboomPlayerProjectile || projectile is BombPlayerProjectile || projectile is ExplosionPlayerProjectile;
                if (!wallCollision.Equals(floorCollision) && !exception) handleCollision(projectile,game);
            }
        }

        public void handleCollision(IPlayerProjectile projectile, GameManager game)
        {
            projectile.collide(game);   
        }
    }
}
