using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace testMonogame
{
    public class EnemyProjectileCollisionHandler
    {
        Game1 game;
        public EnemyProjectileCollisionHandler(Game1 game)
        {
            this.game = game;
        }

        public void handlePlayerCollision(IEnemyProjectile proj, IPlayer player)
        {
            //test for collision
            Console.WriteLine("proj:" + proj.getDestRect().ToString());
            Console.WriteLine("player: " + player.getDestRect().ToString());
            if (player.getDestRect().Intersects(proj.getDestRect()))
            {
                proj.doDamage(player);
                proj.delete(game);
            }

            
        }
    }
}
