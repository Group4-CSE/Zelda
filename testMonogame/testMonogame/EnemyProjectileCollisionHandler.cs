using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using testMonogame.Interfaces;
namespace testMonogame
{
    public class EnemyProjectileCollisionHandler
    {
        GameManager game;
        EnemyProjectileWallCollision wallHandler;

        public EnemyProjectileCollisionHandler(GameManager game)
        {
            this.game = game;
            wallHandler = new EnemyProjectileWallCollision();
        }

        public void handleEnemyProjCollision(IRoom room, IPlayer player)
        {
            wallHandler.detectCollision(room.GetEnemeyProjectile(), room.GetWallDestRect(), room.GetFloorDestRect(), game);

            IEnemyProjectile[] enemyprojArry = room.GetEnemeyProjectile().ToArray();

            foreach(IEnemyProjectile proj in enemyprojArry)
            {
                this.handlePlayerCollision(proj, player);
            }
        }
        public void handlePlayerCollision(IEnemyProjectile proj, IPlayer player)
        {
            //test for collision
            if (player.getDestRect().Intersects(proj.getDestRect()))
            {
                proj.doDamage(player);
                proj.collide(game);
            }

            
        }
    }
}
