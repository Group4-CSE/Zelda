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
            IPlayerProjectile[] playerProjArray = room.GetPlayerProjectiles().ToArray();
            foreach(IEnemyProjectile enemyProj in enemyprojArry)
            {
                foreach(IPlayerProjectile playerProj in playerProjArray)
                {
                    if (playerProj is RupeeShieldPlayerProjectile && playerProj.getDestRect().Intersects(enemyProj.getDestRect())) handleRupeeShieldBlock(enemyProj, playerProj);
                }
            }
        }
        public void handlePlayerCollision(IEnemyProjectile proj, IPlayer player)
        {
            //test for collision
            if (player.getDestRect().Intersects(proj.getDestRect()))
            {
                proj.doDamage(player);
                proj.collide(game);
                //add code to see if player dies later
            }

            
        }
        public void handleRupeeShieldBlock(IEnemyProjectile proj, IPlayerProjectile r)
        {
            proj.collide(game);
            r.collide(game);
        }
    }
}
